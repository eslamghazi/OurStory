namespace OurStory.Services;

public class MessagesService : IMessages
{
    private readonly ApplicationDbContext _context;
    private new List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
    private long _maxAllowedPosterSize = 1048576;

    public MessagesService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Fetch all non-deleted messages through SignalR
    public async Task<IEnumerable<TB_Messages>> GetMessagesBetweenUsers(int userId1, int userId2)
    {
        return await _context.TB_Messages
            .Where(m => ((m.ID_Lovers_Sender_TB == userId1 && m.ID_Lovers_Receiver_TB == userId2) ||
                         (m.ID_Lovers_Sender_TB == userId2 && m.ID_Lovers_Receiver_TB == userId1)))
            .Include(x => x.TB_FilesPath)
            .OrderBy(m => m.DateCreatedAt)
            .ToListAsync();
    }

    public async Task<TB_Messages> SendMessageAsync(MessageDTO messageDTO)
    {
        var message = new TB_Messages
        {
            ID_Lovers_Sender_TB = messageDTO.SenderId,
            ID_Lovers_Receiver_TB = messageDTO.ReceiverId,
            Text = messageDTO.Text,
            DateCreatedAt = messageDTO.DateCreatedAt ?? DateTime.Now,
            IsMessageDeleted = messageDTO.IsMessageDeleted ?? false
        };

        // Handle file uploads
        if (messageDTO.Files != null && messageDTO.Files.Any())
        {
            message.TB_FilesPath = await HandleFileUploadAsync<TB_Messages, TB_FilePaths>(
                messageDTO.Files,
            nameof(TB_Messages),
                $"MessagesBetween{messageDTO.SenderId},{messageDTO.ReceiverId}",
            "Messages",
                message.TB_FilesPath,
                _context.TB_FilePaths,
                item => new TB_FilePaths()
            );
        }

        _context.TB_Messages.Add(message);
        await _context.SaveChangesAsync();

        return message;
    }

    public async Task<TB_Messages> UpdateMessageAsync(MessageDTO messageDTO)
    {
        var message = await _context.TB_Messages.FindAsync(messageDTO.Id);
        if (message == null || message.IsMessageDeleted.Value)
        {
            return null;
        }

        message.Text = messageDTO.Text;
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<bool> DeleteMessageAsync(int messageId)
    {
        var message = await _context.TB_Messages
            .Include(x => x.TB_FilesPath)
            .FirstOrDefaultAsync(x => x.Id == messageId);

        if (message == null)
        {
            return false;
        }

        // Call the generic method to delete files from both the server and the database
        await DeleteFilesAsync<TB_Messages, TB_FilePaths>(
            message.TB_FilesPath,
            $"Messages/Messages between {message.ID_Lovers_Sender_TB}, {message.ID_Lovers_Receiver_TB}",
            _context.TB_FilePaths
        );

        message.IsMessageDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    // Method to save uploaded file and return file URL
    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        // Ensure directory exists
        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }

        var filePath = Path.Combine(uploadsPath, fileName);
        using (var fileStreamOutput = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fileStreamOutput);
        }

        return $"/uploads/{fileName}"; // Return relative file URL
    }


    private async Task<List<TFilePath>> HandleFileUploadAsync<TEntity, TFilePath>(
       IEnumerable<IFormFile> files,
       string entityName,
       string entityTitle,
       string folderPath,
       List<TFilePath> existingFilePaths,
       DbSet<TFilePath> filePathsDbSet,
       Func<IFormFile, TFilePath> createFilePathObject)
       where TEntity : class
       where TFilePath : class
    {
        if (files == null || !files.Any()) return existingFilePaths ?? new List<TFilePath>();

        // Initialize the list if it's null (for first-time blog creation)
        existingFilePaths ??= new List<TFilePath>();

        foreach (var item in files)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(item.FileName).ToLower()))
                throw new Exception("Only .png and .jpg images are allowed!");

            if (item.Length > _maxAllowedPosterSize)
                throw new Exception("Max allowed size for poster is 1MB!");

            // Path to save the file on the server
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{folderPath}/{entityTitle}");

            // Ensure the folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Check if a file with the same name already exists
            var existingFile = existingFilePaths.FirstOrDefault(f => ((dynamic)f).Name == item.FileName);
            if (existingFile != null)
            {
                // Safely extract and delete the old file if it exists
                var oldFilePath = Path.Combine(uploadsFolder, Path.GetFileName(((dynamic)existingFile).Path));
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                filePathsDbSet.Remove(existingFile);
                existingFilePaths.Remove(existingFile);
            }

            // Generate a unique file name for the new file
            var fileName = $"{Guid.NewGuid()}_{item.FileName}";

            // Full path to save the file
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Save the file to the server asynchronously
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await item.CopyToAsync(fileStream);
            }

            // Generate the file URL
            var fileUrl = $"{folderPath}/{entityTitle}/{fileName}";

            // Create a new file path entry using the provided function
            var newFilePath = createFilePathObject(item);
            ((dynamic)newFilePath).Name = item.FileName;
            ((dynamic)newFilePath).Path = fileUrl;

            // Add the new file path entry
            existingFilePaths.Add(newFilePath);
        }

        return existingFilePaths;
    }

    public async Task DeleteFilesAsync<TEntity, TFilePath>(
    List<TFilePath> filePaths,
    string folderPath,
    DbSet<TFilePath> filePathsDbSet)
    where TEntity : class
    where TFilePath : class
    {
        if (filePaths == null || !filePaths.Any())
            return;

        // Path to the uploads folder
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{folderPath}");

        foreach (var file in filePaths)
        {
            var fileName = Path.GetFileName(((dynamic)file).Path);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Delete the file from the server
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove the file path entry from the database
            filePathsDbSet.Remove(file);
        }

        // Save changes to remove file references from the database
        await _context.SaveChangesAsync();
    }


}

