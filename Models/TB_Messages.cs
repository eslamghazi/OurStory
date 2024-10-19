namespace OurStory.Models;

public class TB_Messages
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public string? Text { get; set; }

    [Column(Order = 2)]
    public DateTime DateCreatedAt { get; set; }

    [Column(Order = 3)]
    // New field to track if the message is deleted (soft delete)
    public bool? IsMessageDeleted { get; set; } = false;

    [Column(Order = 4)]
    // New field to track if the message is deleted (soft delete)
    public bool? IsSeen { get; set; } = false;

    [Column(Order = 5)]
    // New field to track if the message is deleted (soft delete)
    public DateTime? SeenAt { get; set; }

    [Column(Order = 6)]
    public List<TB_FilePaths>? TB_FilesPath { get; set; }

    [Column(Order = 7)]
    [ForeignKey("TB_Lovers_Sender")]
    public int? ID_Lovers_Sender_TB { get; set; }
    // Navigation property (optional)
    public TB_Lovers TB_Lovers_Sender { get; set; }

    [Column(Order = 8)]
    [ForeignKey("TB_Lovers_Receiver")]
    public int? ID_Lovers_Receiver_TB { get; set; }
    // Navigation property (optional)
    public TB_Lovers TB_Lovers_Receiver { get; set; }

}
