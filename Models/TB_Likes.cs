namespace OurStory.Models;

public class TB_Likes
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    [ForeignKey("LK_LikesTypes")]
    public int? ID_LK_LikesTypes { get; set; } = 0;
    public LK_LikesTypes LK_LikesTypes { get; set; }

    [Column(Order = 2)]
    [ForeignKey("TB_Lover")]
    public int? ID_Lover_TB { get; set; } = 0;
    public TB_Lovers TB_Lover { get; set; }

}
