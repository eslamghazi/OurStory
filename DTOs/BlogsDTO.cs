namespace ourStory.DTOs;

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
    public TB_Lovers Lovers { get; set; }

}
