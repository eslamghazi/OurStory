namespace OurStory.DTOs;

public class UpdateLoverDTO
{

    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public List<IFormFile>? Files { get; set; }

    public IFormFile? ProfilePicture { get; set; }

    public DescriptionsDTO? Description { get; set; }

}

public class DescriptionsDTO
{
    public int? Id { get; set; }
    public string? Description { get; set; }
    public DateTime? DateCreatedAt { get; set; }

}