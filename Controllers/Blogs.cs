namespace OurStory.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User, Admin")]
public class Blogs : ControllerBase
{
    private IBlogs _blogsService;

    public Blogs(IBlogs blogsService)
    {
        _blogsService = blogsService;
    }

    [HttpGet("GetAllBlogs")]
    public async Task<IActionResult> GetAllBlogs(int blogType = 0, int events = 0, int published = 0, int lover = 0)
    {
        var Blogs = await _blogsService.GetAll(blogType, events, published, lover);

        if (!Blogs.Any())
            throw new Exception("لا توجد مدونات");

        List<BlogsDTO> blogsDTO = new List<BlogsDTO>();

        blogsDTO = Blogs.Select(item => new BlogsDTO
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Blog_Type = new Lookup { Id = item.LK_Blog_Type.Id, NameAr = item.LK_Blog_Type.NameAr, NameEng = item.LK_Blog_Type.NameEng },
            Published = new Lookup { Id = item.LK_Published.Id, NameAr = item.LK_Published.NameAr, NameEng = item.LK_Published.NameEng },
            Events = new Lookup { Id = item.LK_Events.Id, NameAr = item.LK_Events.NameAr, NameEng = item.LK_Events.NameEng },
            ItsDate = item.ItsDate,
            DateCreatedAt = item.DateCreatedAt,
            FilesPath = item.TB_FilesPath,
            Lovers = new TB_Lovers { Id = item.TB_Lovers.Id, Name = item.TB_Lovers.Name },

        }).ToList();

        return Ok(new { status = 200, Data = blogsDTO });

    }

    [HttpGet("GetBlogById/{id}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        var Blog = await _blogsService.GetById(id);

        if (Blog == null)
            throw new Exception("لا توجد مدونه بهذا الرقم");

        BlogsDTO blogsDTO = new BlogsDTO();

        blogsDTO = new BlogsDTO()
        {
            Id = Blog.Id,
            Title = Blog.Title,
            Description = Blog.Description,
            Blog_Type = new Lookup { Id = Blog.LK_Blog_Type.Id, NameAr = Blog.LK_Blog_Type.NameAr, NameEng = Blog.LK_Blog_Type.NameEng },
            Published = new Lookup { Id = Blog.LK_Published.Id, NameAr = Blog.LK_Published.NameAr, NameEng = Blog.LK_Published.NameEng },
            Events = new Lookup { Id = Blog.LK_Events.Id, NameAr = Blog.LK_Events.NameAr, NameEng = Blog.LK_Events.NameEng },
            ItsDate = Blog.ItsDate,
            DateCreatedAt = Blog.DateCreatedAt,
            FilesPath = Blog.TB_FilesPath,
            Lovers = new TB_Lovers { Id = Blog.TB_Lovers.Id, Name = Blog.TB_Lovers.Name },

        };

        return Ok(new { status = 200, Data = blogsDTO });

    }

    [HttpPost(template: "AddBlog")]
    public async Task<IActionResult> AddBlog([FromForm] AddUpdateBlogsDTO DTO)
    {

        var Blog = await _blogsService.AddAsync(DTO);

        if (Blog == null)
            throw new Exception("لا يمكن إضافة المدونة");

        return Ok(Blog);
    }

    [HttpPost(template: "UpdateBlog")]
    public async Task<IActionResult> UpdateBlog([FromForm] AddUpdateBlogsDTO DTO)
    {

        var Blog = await _blogsService.UpdateAsync(DTO);

        if (Blog == null)
            throw new Exception("لا يمكن تعديل المدونة");

        return Ok(Blog);
    }

    [HttpDelete("DeleteBlog")]
    public async Task<IActionResult> DeleteBlog(int id)
    {

        var Blog = await _blogsService.DeleteAsync(id);

        if (Blog == null)
            throw new Exception("لا يمكن حذف المدونة");

        return Ok(Blog);
    }

    [HttpDelete("DeleteFile")]
    public async Task<IActionResult> DeleteFile(int id)
    {

        var File = await _blogsService.DeleteFile(id);

        if (File == null)
            throw new Exception("لا يمكن حذف الملف");

        return Ok(File);
    }

}


