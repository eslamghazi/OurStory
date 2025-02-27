﻿namespace OurStory.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User, Admin")]
public class Blogs : ControllerBase
{
    private readonly IBlogs _blogsService;

    public Blogs(IBlogs blogsService)
    {
        _blogsService = blogsService;
    }

    [HttpGet("GetAllBlogs")]
    public async Task<IActionResult> GetAllBlogs(int blogType = 0, int events = 0, int published = 0, int lover = 0)
    {
        var Blogs = await _blogsService.GetAll(blogType, events, published, lover);

        if (!Blogs.Any())
            return BadRequest(new { StatusCode = 500, Message = "لا توجد مدونات او انه حدث خطأ ما!" });

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
            Comments = item.TB_Comments,
            CommentsCount = item.TB_Comments.Count(),
            Likes = item.TB_Likes,
            LikesCount = item.TB_Likes.Count(),
        }).ToList();


        return Ok(new { StatusCode = 200, Data = blogsDTO });

    }

    [HttpGet("GetBlogById/{id}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        var Blog = await _blogsService.GetById(id);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا توجد مدونه بهذا الرقم او انه حدث خطأ ما!" });

        BlogsDTO blogsDTO = new BlogsDTO()
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
            Comments = Blog.TB_Comments,
            CommentsCount = Blog.TB_Comments.Count(),
            Likes = Blog.TB_Likes,
            LikesCount = Blog.TB_Likes.Count(),

        };

        return Ok(new { StatusCode = 200, Data = blogsDTO });

    }

    [HttpPost(template: "AddBlog")]
    public async Task<IActionResult> AddBlog([FromForm] AddUpdateBlogsDTO DTO)
    {

        var Blog = await _blogsService.AddAsync(DTO);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن إضافة المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });

    }

    [HttpPost(template: "UpdateBlog")]
    public async Task<IActionResult> UpdateBlog([FromForm] AddUpdateBlogsDTO DTO)
    {

        var Blog = await _blogsService.UpdateAsync(DTO);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن تعديل المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });
    }

    [HttpDelete("DeleteBlog/{id}")]
    public async Task<IActionResult> DeleteBlog(int id)
    {

        var Blog = await _blogsService.DeleteAsync(id);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن حذف المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });
    }

    [HttpDelete("DeleteFile/{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {

        var File = await _blogsService.DeleteFile(id);

        if (File == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن حذف الملف او انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = new { File } });

    }

    [HttpPost(template: "AddComment")]
    public async Task<IActionResult> AddCommentBlogAsync([FromBody] CommentBlogDTO DTO)
    {

        var Blog = await _blogsService.AddCommentBlogAsync(DTO);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن اضافة تعليف علي المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });
    }

    [HttpPost(template: "UpdateComment")]
    public async Task<IActionResult> UpdateCommentBlogAsync([FromBody] CommentBlogDTO DTO)
    {

        var Blog = await _blogsService.UpdateCommentBlogAsync(DTO);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن تعديل التعليف علي المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });
    }

    [HttpPost(template: "UpdateLike")]
    public async Task<IActionResult> UpdateLikeBlogAsync([FromBody] LikeBlogDTO DTO)
    {

        var Blog = await _blogsService.UpdateLikeBlogAsync(DTO);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن تعديل الاعجاب علي المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });
    }

    [HttpDelete(template: "DeleteComment")]
    public async Task<IActionResult> DeleteCommentBlogAsync(CommentBlogDTO CommentBlogDTO)
    {

        var Blog = await _blogsService.DeleteCommentBlogAsync(CommentBlogDTO);

        if (Blog == null)
            return BadRequest(new { StatusCode = 500, Message = "لا يمكن حذف التعليق من المدونة او انه حدث خطأ ما!" });

        var CommentsCount = Blog.TB_Comments.Count();

        var LikesCount = Blog.TB_Likes.Count();

        return Ok(new { StatusCode = 200, Data = new { Blog, LikesCount, CommentsCount } });
    }

}


