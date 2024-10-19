namespace OurStory.Models;

public class TB_FilePaths
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public string Name { get; set; }

    [Column(Order = 2)]
    public string Path { get; set; }

    //[Column(Order = 3)]
    //[ForeignKey("TB_Lover")]
    //public int? BlogId { get; set; }
    //public TB_Blogs TB_Blog { get; set; }

    //[Column(Order = 4)]
    //[ForeignKey("TB_Lover")]
    //public int? LoverId { get; set; }
    //public TB_Lovers TB_Lover { get; set; }
}
