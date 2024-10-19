namespace OurStory.Models;

public class TB_Blogs
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public string Title { get; set; }

    [Column(Order = 2)]
    public string Description { get; set; }

    [Column(Order = 3)]
    [ForeignKey("LK_Blog_Type")]
    public int? ID_Blog_Type_LK { get; set; } = 0;
    public LK_BlogTypeLookup LK_Blog_Type { get; set; }

    [Column(Order = 4)]
    [ForeignKey("LK_Published")]
    public int? ID_Published_LK { get; set; } = 0;
    public LK_PublishedLookup LK_Published { get; set; }

    [Column(Order = 5)]
    [ForeignKey("LK_Events")]
    public int? ID_Events_LK { get; set; } = 0;
    public LK_EventsLookup LK_Events { get; set; }

    [Column(Order = 6)]
    public DateTime? ItsDate { get; set; }

    [Column(Order = 7)]
    public DateTime DateCreatedAt { get; set; }

    [Column(Order = 8)]
    public List<TB_FilePaths>? TB_FilesPath { get; set; }

    [Column(Order = 9)]
    [ForeignKey("TB_Lovers")]
    public int? ID_Lovers_TB { get; set; } = 0;
    public TB_Lovers TB_Lovers { get; set; }

}
