namespace ourStory.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TB_Lovers>().HasData(
    new TB_Lovers { Id = 1, Name = "Semsem", Password = "SemsemFallInLoveWithHisBascota", Role = "User" },
    new TB_Lovers { Id = 2, Name = "Bascota", Password = "BascotaFallInLoveWithHerSemsem", Role = "User" },
    new TB_Lovers { Id = 3, Name = "HeroSuperAdmin", Password = "HeroWhatIsHero", Role = "Admin" });

        modelBuilder.Entity<LK_BlogTypeLookup>().HasData(
    new LK_BlogTypeLookup { Id = 1, NameAr = "غير مخصص", NameEng = "NotSpecific" },
    new LK_BlogTypeLookup { Id = 2, NameAr = "احداثنا", NameEng = "OurEvents" },
    new LK_BlogTypeLookup { Id = 3, NameAr = "احلامنا", NameEng = "OurDreams" },
    new LK_BlogTypeLookup { Id = 4, NameAr = "اسرارنا", NameEng = "OurSecrets" },
    new LK_BlogTypeLookup { Id = 5, NameAr = "بدايتنا", NameEng = "OurStart" },
    new LK_BlogTypeLookup { Id = 6, NameAr = "قصتنا", NameEng = "OurStory" });

        modelBuilder.Entity<LK_EventsLookup>().HasData(
    new LK_EventsLookup { Id = 1, NameAr = "غير مرتبطه", NameEng = "notRelated" },
    new LK_EventsLookup { Id = 2, NameAr = "مرت", NameEng = "Passed" },
    new LK_EventsLookup { Id = 3, NameAr = "قادمه", NameEng = "OurDreams" });

        modelBuilder.Entity<LK_PublishedLookup>().HasData(
    new LK_EventsLookup { Id = 1, NameAr = "غير مرتبطه", NameEng = "notRelated" },
    new LK_PublishedLookup { Id = 2, NameAr = "ليس بعد", NameEng = "NotYet" },
    new LK_PublishedLookup { Id = 3, NameAr = "منشوره", NameEng = "Published" });

        modelBuilder.Entity<TB_Blogs>().HasData(
    new TB_Blogs { Id = 1, Title = "aa", Description = "aaaaaaaaaaaa", ID_Blog_Type_LK = 1, ID_Events_LK = 1, ID_Published_LK = 1, DateCreatedAt = DateTime.Now, ID_Lovers_TB = 1 },
    new TB_Blogs { Id = 2, Title = "bb", Description = "bbbbbbbbbbbb", ID_Blog_Type_LK = 1, ID_Events_LK = 1, ID_Published_LK = 1, DateCreatedAt = DateTime.Now, ID_Lovers_TB = 1 });

    }

    public DbSet<TB_Lovers> TB_Lovers { get; set; }
    public DbSet<TB_Descriptions> TB_Descriptions { get; set; }
    public DbSet<TB_Blogs> TB_OurBlogs { get; set; }
    public DbSet<TB_FilePaths> TB_FilePaths { get; set; }
    public DbSet<LK_BlogTypeLookup> LK_BlogTypeLookup { get; set; }
    public DbSet<LK_EventsLookup> LK_EventsLookup { get; set; }
    public DbSet<LK_PublishedLookup> LK_PublishedLookup { get; set; }

}
