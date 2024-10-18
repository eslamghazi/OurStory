namespace ourStory.Models;

public class TB_Descriptions
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public string Description { get; set; }

    [Column(Order = 2)]
    public DateTime DateCreatedAt { get; set; }


}
