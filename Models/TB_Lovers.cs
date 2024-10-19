namespace OurStory.Models;

public class TB_Lovers
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public string Name { get; set; }

    [Column(Order = 2)]
    public string Password { get; set; }

    [Column(Order = 3)]
    public string Role { get; set; }

    [Column(Order = 4)]
    public List<TB_FilePaths>? TB_FilesPath { get; set; } = new List<TB_FilePaths>();

    [Column(Order = 5)]
    public List<TB_Descriptions>? TB_Description { get; set; } = new List<TB_Descriptions>();



}

