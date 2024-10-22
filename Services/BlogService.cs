namespace OurStory.Services;

public class BlogService : IBlogs
{

    private readonly ApplicationDbContext _DbContext;

    private new List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
    private long _maxAllowedPosterSize = 1048576;

    public BlogService(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<IEnumerable<TB_Blogs>> GetAll(int blogType = 0, int events = 0, int published = 0, int lover = 0)
    {
        List<TB_Blogs> Blogs = new List<TB_Blogs>();

        Blogs = await _DbContext.TB_OurBlogs
            .Include(x => x.LK_Blog_Type)
            .Include(x => x.LK_Events)
            .Include(x => x.LK_Published)
            .Include(x => x.TB_FilesPath)
            .Include(x => x.TB_Lovers)
            .Include(x => x.TB_Likes)
            .ThenInclude(y => y.TB_Lover)
            .Include(x => x.TB_Likes)
            .ThenInclude(y => y.LK_LikesTypes)
            .Include(x => x.TB_Comments)
            .ThenInclude(y => y.TB_Lover)
            .ToListAsync();

        if (blogType != 0)
        {
            Blogs = Blogs.Where(x => x.ID_Blog_Type_LK == blogType).ToList();

            if (Blogs.Count == 0)
            {
                return Blogs = new List<TB_Blogs>();
            }

        }

        if (events != 0)
        {
            Blogs = Blogs.Where(x => x.ID_Events_LK == events)
                .ToList();

            if (Blogs.Count == 0)
            {
                return Blogs = new List<TB_Blogs>();
            }

        }

        if (published != 0)
        {
            Blogs = Blogs.Where(x => x.ID_Published_LK == published)
                .ToList();

            if (Blogs.Count == 0)
            {
                return Blogs = new List<TB_Blogs>();
            }

        }

        if (lover != 0)
        {
            Blogs = Blogs.Where(x => x.TB_Lovers.Id == lover)
                 .ToList();

            if (Blogs.Count == 0)
            {
                return Blogs = new List<TB_Blogs>();
            }

        }

        return Blogs;
    }

    public async Task<TB_Blogs> GetById(int id)
    {
        var Blog = await _DbContext.TB_OurBlogs
                .Include(x => x.LK_Blog_Type)
                .Include(x => x.LK_Events)
                .Include(x => x.LK_Published)
                .Include(x => x.TB_FilesPath)
                .Include(x => x.TB_Lovers)
            .Include(x => x.TB_Likes)
            .ThenInclude(y => y.TB_Lover)
            .Include(x => x.TB_Likes)
            .ThenInclude(y => y.LK_LikesTypes)
            .Include(x => x.TB_Comments)
            .ThenInclude(y => y.TB_Lover)
            .FirstOrDefaultAsync(x => x.Id == id);

        return Blog;

    }

    public async Task<TB_Blogs> AddAsync(AddUpdateBlogsDTO blogDTO)
    {
        if (blogDTO == null)
        {
            throw new Exception("blogDTO is null.");
        }

        var Blog = new TB_Blogs();

        // Initialize the TB_FilesPath list if it is null
        Blog.TB_FilesPath = Blog.TB_FilesPath ?? new List<TB_FilePaths>();

        // Handle file uploads
        if (blogDTO.Files != null && blogDTO.Files.Any())
        {
            Blog.TB_FilesPath = await HandleFileUploadAsync<TB_Blogs, TB_FilePaths>(
                blogDTO.Files,
                nameof(TB_Blogs),
                blogDTO.Title ?? Blog.Title,
                "Blogs",
                Blog.TB_FilesPath,
                _DbContext.TB_FilePaths,
                item => new TB_FilePaths()
            );
        }

        if (!string.IsNullOrEmpty(blogDTO.Title))
            Blog.Title = blogDTO.Title;

        if (!string.IsNullOrEmpty(blogDTO.Description))
            Blog.Description = blogDTO.Description;

        if (blogDTO.ItsDate.HasValue)
            Blog.ItsDate = blogDTO.ItsDate.Value;

        if (blogDTO.DateCreatedAt.HasValue)
            Blog.DateCreatedAt = blogDTO.DateCreatedAt.Value;
        else
            Blog.DateCreatedAt = DateTime.Now;


        if (blogDTO.Blog_Type.HasValue)
            Blog.ID_Blog_Type_LK = blogDTO.Blog_Type.Value;

        if (blogDTO.Published.HasValue)
            Blog.ID_Published_LK = blogDTO.Published.Value;

        if (blogDTO.Event.HasValue)
            Blog.ID_Events_LK = blogDTO.Event.Value;

        if (blogDTO.Lover.HasValue)
            Blog.ID_Lovers_TB = blogDTO.Lover.Value;

        await _DbContext.TB_OurBlogs.AddAsync(Blog);

        //try
        //{
        //    await _DbContext.SaveChangesAsync();
        //}
        //catch (DbUpdateException ex)
        //{
        //    var innerException = ex.InnerException?.Message;
        //    throw new Exception($"Error while saving changes: {innerException ?? ex.Message}");
        //}

        await _DbContext.SaveChangesAsync();

        return Blog;
    }

    public async Task<TB_Blogs> UpdateAsync(AddUpdateBlogsDTO blogDTO)
    {
        if (blogDTO is null)
        {
            throw new Exception($"blogDTO is NUll.");
        }

        var Blog = await _DbContext.TB_OurBlogs
                .Include(x => x.LK_Blog_Type)
                .Include(x => x.LK_Events)
                .Include(x => x.LK_Published)
                .Include(x => x.TB_FilesPath)
                .Include(x => x.TB_Lovers)
                            .Include(x => x.TB_Likes)
            .ThenInclude(y => y.TB_Lover)
            .Include(x => x.TB_Likes)
            .ThenInclude(y => y.LK_LikesTypes)
            .Include(x => x.TB_Comments)
            .ThenInclude(y => y.TB_Lover)

            .FirstOrDefaultAsync(x => x.Id == blogDTO.Id);

        if (Blog == null)
        {
            throw new Exception($"Blog with ID {blogDTO.Id} not found.");
        }

        // Handle file uploads
        if (blogDTO.Files != null && blogDTO.Files.Any())
        {
            Blog.TB_FilesPath = await HandleFileUploadAsync<TB_Blogs, TB_FilePaths>(
                blogDTO.Files,
                nameof(TB_Blogs),
                blogDTO.Title ?? Blog.Title,
                "Blogs",
                Blog.TB_FilesPath,
                _DbContext.TB_FilePaths,
                item => new TB_FilePaths()
            );
        }

        if (!string.IsNullOrEmpty(blogDTO.Title))
            Blog.Title = blogDTO.Title;

        if (!string.IsNullOrEmpty(blogDTO.Description))
            Blog.Description = blogDTO.Description;

        if (blogDTO.ItsDate.HasValue)
            Blog.ItsDate = blogDTO.ItsDate.Value;

        if (blogDTO.DateCreatedAt.HasValue)
            Blog.DateCreatedAt = blogDTO.DateCreatedAt.Value;
        else
            Blog.DateCreatedAt = DateTime.Now;

        if (blogDTO.Blog_Type.HasValue)
            Blog.ID_Blog_Type_LK = blogDTO.Blog_Type.Value;  // Update if Blog_Type is provided

        if (blogDTO.Published.HasValue)
            Blog.ID_Published_LK = blogDTO.Published.Value;  // Update if Published is provided

        if (blogDTO.Event.HasValue)
            Blog.ID_Events_LK = blogDTO.Event.Value;  // Update if Event is provided

        if (blogDTO.Lover.HasValue)
            Blog.ID_Lovers_TB = blogDTO.Lover.Value;  // Update if Lover is provided

        _DbContext.TB_OurBlogs.Update(Blog);

        await _DbContext.SaveChangesAsync();

        return Blog;

    }

    public async Task<TB_Blogs> DeleteAsync(int id)
    {
        var blogToBeRemoved = await _DbContext.TB_OurBlogs
            .Include(x => x.TB_FilesPath)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (blogToBeRemoved == null)
        {
            throw new Exception("Blog not found");
        }

        // Call the generic method to delete files from both the server and the database
        await DeleteFilesAsync<TB_Blogs, TB_FilePaths>(
            blogToBeRemoved.TB_FilesPath,
            $"Blogs/{blogToBeRemoved.Title}",
            _DbContext.TB_FilePaths
        );

        // Remove the blog itself from the database
        _DbContext.TB_OurBlogs.Remove(blogToBeRemoved);

        await _DbContext.SaveChangesAsync();

        return blogToBeRemoved;
    }

    public async Task<TB_FilePaths> DeleteFile(int id)
    {
        var FileToBeRemoved = await _DbContext.TB_FilePaths
    .FirstOrDefaultAsync(x => x.Id == id);

        if (FileToBeRemoved == null)
        {
            throw new Exception("File not found");
        }

        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{FileToBeRemoved.Path}");
        if (File.Exists(oldFilePath))
        {
            File.Delete(oldFilePath);
        }

        // Remove the blog itself from the database
        _DbContext.TB_FilePaths.Remove(FileToBeRemoved);

        await _DbContext.SaveChangesAsync();

        return FileToBeRemoved;

    }

    public async Task<TB_Blogs> AddCommentBlogAsync(CommentBlogDTO CommentBlogDTO)
    {
        if (CommentBlogDTO == null)
        {
            throw new Exception("blogDTO is null.");
        }

        var Blog = await _DbContext.TB_OurBlogs
            .Include(x => x.TB_Likes)
            .Include(x => x.TB_Comments)
            .Include(x => x.TB_Lovers)
            .FirstOrDefaultAsync(x => x.Id == CommentBlogDTO.BlogId);

        if (Blog == null)
            throw new Exception($"Blog with ID {CommentBlogDTO.BlogId} not found.");

        var Lover = await _DbContext.TB_Lovers
            .Include(x => x.TB_FilesPath_ProfilePicture)
            .FirstOrDefaultAsync(x => x.Id == CommentBlogDTO.LoverId);

        if (Lover == null)
            throw new Exception($"Lover with ID {CommentBlogDTO.LoverId} not found.");

        TB_Comments TB_Comments = new TB_Comments();

        TB_Comments.ID_Lover_TB = CommentBlogDTO.LoverId;
        TB_Comments.Comment = CommentBlogDTO.Comment;

        Blog.TB_Comments.Add(TB_Comments);

        await _DbContext.SaveChangesAsync();

        return Blog;

    }

    public async Task<TB_Blogs> UpdateCommentBlogAsync(CommentBlogDTO CommentBlogDTO)
    {
        if (CommentBlogDTO == null)
        {
            throw new Exception("blogDTO is null.");
        }

        var Blog = await _DbContext.TB_OurBlogs
            .Include(x => x.TB_Likes)
            .Include(x => x.TB_Comments)
            .Include(x => x.TB_Lovers)
            .FirstOrDefaultAsync(x => x.Id == CommentBlogDTO.BlogId);

        if (Blog == null)
            throw new Exception($"Blog with ID {CommentBlogDTO.BlogId} not found.");

        var Lover = await _DbContext.TB_Lovers
            .Include(x => x.TB_FilesPath_ProfilePicture)
            .FirstOrDefaultAsync(x => x.Id == CommentBlogDTO.LoverId);

        if (Lover == null)
            throw new Exception($"Lover with ID {CommentBlogDTO.LoverId} not found.");

        var CurrentComment = Blog.TB_Comments.FirstOrDefault(x => x.Id == CommentBlogDTO.CommentId);

        if (CurrentComment == null)
            throw new Exception($"Comment with ID {CommentBlogDTO.CommentId} not found.");

        CurrentComment.Comment = CommentBlogDTO.Comment;

        await _DbContext.SaveChangesAsync();

        return Blog;

    }

    public async Task<TB_Blogs> UpdateLikeBlogAsync(LikeBlogDTO LikeBlogDTO)
    {
        if (LikeBlogDTO == null)
        {
            throw new Exception("blogDTO is null.");
        }

        var Blog = await _DbContext.TB_OurBlogs
            .Include(x => x.TB_Likes)
            .ThenInclude(x => x.TB_Lover)
            .Include(x => x.TB_Likes)
            .ThenInclude(x => x.LK_LikesTypes)
            .Include(x => x.TB_Comments)
            .Include(x => x.TB_Lovers)
            .FirstOrDefaultAsync(x => x.Id == LikeBlogDTO.BlogId);

        if (Blog == null)
            throw new Exception($"Blog with ID {LikeBlogDTO.BlogId} not found.");

        var Lover = await _DbContext.TB_Lovers
            .Include(x => x.TB_FilesPath_ProfilePicture)
            .FirstOrDefaultAsync(x => x.Id == LikeBlogDTO.LoverId);

        if (Lover == null)
            throw new Exception($"Lover with ID {LikeBlogDTO.LoverId} not found.");

        var CurrentLike = Blog.TB_Likes.FirstOrDefault(x => x.Id == LikeBlogDTO.LikeId);

        if (CurrentLike == null)
        {
            TB_Likes TB_Likes = new TB_Likes();

            TB_Likes.ID_Lover_TB = LikeBlogDTO.LoverId;
            TB_Likes.ID_LK_LikesTypes = LikeBlogDTO.LikeType;

            Blog.TB_Likes.Add(TB_Likes);
        }
        else
        {
            if (LikeBlogDTO.IsDelete.Value)
            {
                Blog.TB_Likes.Remove(CurrentLike);
            }
            else
            {
                CurrentLike.ID_LK_LikesTypes = LikeBlogDTO.LikeType;
            }

        }

        await _DbContext.SaveChangesAsync();

        return Blog;

    }

    public async Task<TB_Blogs> DeleteCommentBlogAsync(CommentBlogDTO CommentBlogDTO)
    {

        var Blog = await _DbContext.TB_OurBlogs
            .Include(x => x.TB_Likes)
            .Include(x => x.TB_Comments)
            .Include(x => x.TB_Lovers)
            .FirstOrDefaultAsync(x => x.Id == CommentBlogDTO.BlogId);

        if (Blog == null)
            throw new Exception($"Blog with ID {CommentBlogDTO.BlogId} not found.");

        var Comment = await _DbContext.TB_Comments.FirstOrDefaultAsync(x => x.Id == CommentBlogDTO.CommentId);

        if (Comment == null)
            throw new Exception($"Comment with ID {CommentBlogDTO.CommentId} not found.");

        _DbContext.TB_Comments.Remove(Comment);

        await _DbContext.SaveChangesAsync();

        return Blog;

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
        await _DbContext.SaveChangesAsync();
    }


}
