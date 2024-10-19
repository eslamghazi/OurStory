namespace OurStory.Services;

public class LoverService : ILovers
{
    private readonly ApplicationDbContext _DbContext;

    private new List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
    private long _maxAllowedPosterSize = 1048576;

    public LoverService(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }


    public async Task<IEnumerable<TB_Descriptions>> getAllDescriptions(int LoverId)
    {

        var Lover = await _DbContext.TB_Lovers
        .Include(x => x.TB_Description)
    .FirstOrDefaultAsync(x => x.Id == LoverId);

        if (Lover == null)
            throw new Exception($"Lover with ID {LoverId} not found.");

        return Lover.TB_Description.ToList(); ;
    }

    public async Task<TB_Descriptions> UpdateDescription(DescriptionsDTO DescriptionDTO)
    {

        var ExistsDescription = await _DbContext.TB_Descriptions
           .FirstOrDefaultAsync(x => x.Id == DescriptionDTO.Id);

        if (ExistsDescription == null)
            throw new Exception($"Description with ID {DescriptionDTO.Id} not found.");

        ExistsDescription.Description = DescriptionDTO.Description;

        if (DescriptionDTO.DateCreatedAt != null)
            ExistsDescription.DateCreatedAt = DescriptionDTO.DateCreatedAt.Value;
        else
            ExistsDescription.DateCreatedAt = DateTime.Now;

        _DbContext.TB_Descriptions.Update(ExistsDescription);

        await _DbContext.SaveChangesAsync();

        return ExistsDescription;

    }


    public async Task<TB_Descriptions> DeleteDescription(int DescriptionId)
    {
        var Description = await _DbContext.TB_Descriptions
    .FirstOrDefaultAsync(x => x.Id == DescriptionId);

        if (Description == null)
            throw new Exception($"Description with ID {DescriptionId} not found.");

        _DbContext.TB_Descriptions.Remove(Description);

        await _DbContext.SaveChangesAsync();

        return Description;
    }

    public async Task<TB_Lovers> UpdateLover(UpdateLoverDTO LoverDTO, bool IsEditDescription)
    {
        if (LoverDTO is null)
        {
            throw new Exception($"LoverDTO is NUll.");
        }

        var Lover = await _DbContext.TB_Lovers
                .Include(x => x.TB_Description)
            .FirstOrDefaultAsync(x => x.Id == LoverDTO.Id);

        if (Lover == null)
        {
            throw new Exception($"Lover with ID {LoverDTO.Id} not found.");
        }

        // Handle file uploads
        if (LoverDTO.Files != null && LoverDTO.Files.Any())
        {
            Lover.TB_FilesPath = await HandleFileUploadAsync<TB_Lovers, TB_FilePaths>(
                LoverDTO.Files,
                nameof(TB_Lovers),
                LoverDTO.Name ?? Lover.Name,
                "Lovers",
            Lover.TB_FilesPath,
                _DbContext.TB_FilePaths,
                item => new TB_FilePaths()
            );
        }

        if (LoverDTO.Description != null)
        {

            TB_Descriptions Description = new TB_Descriptions();

            if (IsEditDescription)
            {

                Description = Lover.TB_Description
               .FirstOrDefault(x => x.Id == LoverDTO.Description.Id);

                if (Description == null)
                    throw new Exception($"Description with ID {LoverDTO.Description.Id} not found.");

                Description.Description = LoverDTO.Description.Description;

                if (LoverDTO.Description.DateCreatedAt != null)
                    Description.DateCreatedAt = LoverDTO.Description.DateCreatedAt.Value;
                else
                    Description.DateCreatedAt = DateTime.Now;

            }
            else
            {

                Description.Description = LoverDTO.Description.Description;

                if (LoverDTO.Description.DateCreatedAt != null)
                    Description.DateCreatedAt = LoverDTO.Description.DateCreatedAt.Value;
                else
                    Description.DateCreatedAt = DateTime.Now;


            }

        }

        if (!string.IsNullOrEmpty(LoverDTO.Name))
            Lover.Name = LoverDTO.Name;


        if (LoverDTO.Password is not null)
            Lover.Password = LoverDTO.Password;

        if (LoverDTO.Role is not null)
            Lover.Role = LoverDTO.Role;

        _DbContext.TB_Lovers.Update(Lover);

        await _DbContext.SaveChangesAsync();

        return Lover;

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

        // Initialize the list if it's null (for first-time Lover creation)
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

}
