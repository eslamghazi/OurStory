namespace OurStory.DTOs;

public class MessageDTO
{
    public int? Id { get; set; }

    public string? Text { get; set; }

    public DateTime? DateCreatedAt { get; set; }

    public bool? IsMessageDeleted { get; set; } = false;
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public List<TB_FilePaths>? FilesPath { get; set; }
    public List<IFormFile>? Files { get; set; }

}
public class SeenMessageDTO
{
    public int? messageId { get; set; }

    public int? receiverId { get; set; }

    public DateTime? SeenAt { get; set; }

}