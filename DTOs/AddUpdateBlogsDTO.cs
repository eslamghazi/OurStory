namespace OurStory.DTOs;

public class AddUpdateBlogsDTO
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? Blog_Type { get; set; }

    public int? Published { get; set; }

    public int? Event { get; set; }

    public DateTime? DateCreatedAt { get; set; }
    public DateTime? ItsDate { get; set; }

    public List<IFormFile>? Files { get; set; }

    public int? Lover { get; set; }


}
