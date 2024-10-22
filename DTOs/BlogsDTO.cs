namespace OurStory.DTOs;

public class BlogsDTO
{
    public int? Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Lookup Blog_Type { get; set; }

    public Lookup Published { get; set; }

    public Lookup Events { get; set; }

    public DateTime? ItsDate { get; set; }

    public DateTime? DateCreatedAt { get; set; }

    public List<TB_FilePaths>? FilesPath { get; set; }
    public List<IFormFile>? Files { get; set; }
    public List<TB_Comments> Comments { get; set; }
    public int CommentsCount { get; set; }
    public List<TB_Likes> Likes { get; set; }
    public int LikesCount { get; set; }
    public TB_Lovers Lovers { get; set; }

}
public class LikeBlogDTO
{
    public int BlogId { get; set; }

    public int LoverId { get; set; }

    public bool? IsDelete { get; set; } = false;

    public int? LikeId { get; set; }

    public int? LikeType { get; set; } = 1;

}
public class CommentBlogDTO
{
    public int BlogId { get; set; }

    public int LoverId { get; set; }

    public int? CommentId { get; set; }

    public string? Comment { get; set; }

}
