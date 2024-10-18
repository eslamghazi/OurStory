﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ourStory.Models;

#nullable disable

namespace OurStory.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ourStory.Models.Lookups.LK_BlogTypeLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LK_BlogTypeLookup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NameAr = "غير مخصص",
                            NameEng = "NotSpecific"
                        },
                        new
                        {
                            Id = 2,
                            NameAr = "احداثنا",
                            NameEng = "OurEvents"
                        },
                        new
                        {
                            Id = 3,
                            NameAr = "احلامنا",
                            NameEng = "OurDreams"
                        },
                        new
                        {
                            Id = 4,
                            NameAr = "اسرارنا",
                            NameEng = "OurSecrets"
                        },
                        new
                        {
                            Id = 5,
                            NameAr = "بدايتنا",
                            NameEng = "OurStart"
                        },
                        new
                        {
                            Id = 6,
                            NameAr = "قصتنا",
                            NameEng = "OurStory"
                        });
                });

            modelBuilder.Entity("ourStory.Models.Lookups.LK_EventsLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LK_EventsLookup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NameAr = "غير مرتبطه",
                            NameEng = "notRelated"
                        },
                        new
                        {
                            Id = 2,
                            NameAr = "مرت",
                            NameEng = "Passed"
                        },
                        new
                        {
                            Id = 3,
                            NameAr = "قادمه",
                            NameEng = "OurDreams"
                        });
                });

            modelBuilder.Entity("ourStory.Models.Lookups.LK_PublishedLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LK_PublishedLookup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NameAr = "غير مرتبطه",
                            NameEng = "notRelated"
                        },
                        new
                        {
                            Id = 2,
                            NameAr = "ليس بعد",
                            NameEng = "NotYet"
                        },
                        new
                        {
                            Id = 3,
                            NameAr = "منشوره",
                            NameEng = "Published"
                        });
                });

            modelBuilder.Entity("ourStory.Models.TB_Blogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(7);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<int?>("ID_Blog_Type_LK")
                        .HasColumnType("int")
                        .HasColumnOrder(3);

                    b.Property<int?>("ID_Events_LK")
                        .HasColumnType("int")
                        .HasColumnOrder(5);

                    b.Property<int?>("ID_Lovers_TB")
                        .HasColumnType("int")
                        .HasColumnOrder(9);

                    b.Property<int?>("ID_Published_LK")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("ItsDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("ID_Blog_Type_LK");

                    b.HasIndex("ID_Events_LK");

                    b.HasIndex("ID_Lovers_TB");

                    b.HasIndex("ID_Published_LK");

                    b.ToTable("TB_OurBlogs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreatedAt = new DateTime(2024, 10, 18, 9, 17, 36, 270, DateTimeKind.Local).AddTicks(16),
                            Description = "aaaaaaaaaaaa",
                            ID_Blog_Type_LK = 1,
                            ID_Events_LK = 1,
                            ID_Lovers_TB = 1,
                            ID_Published_LK = 1,
                            Title = "aa"
                        },
                        new
                        {
                            Id = 2,
                            DateCreatedAt = new DateTime(2024, 10, 18, 9, 17, 36, 270, DateTimeKind.Local).AddTicks(66),
                            Description = "bbbbbbbbbbbb",
                            ID_Blog_Type_LK = 1,
                            ID_Events_LK = 1,
                            ID_Lovers_TB = 1,
                            ID_Published_LK = 1,
                            Title = "bb"
                        });
                });

            modelBuilder.Entity("ourStory.Models.TB_Descriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(2);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.Property<int?>("TB_LoversId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TB_LoversId");

                    b.ToTable("TB_Descriptions");
                });

            modelBuilder.Entity("ourStory.Models.TB_FilePaths", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<int?>("TB_BlogsId")
                        .HasColumnType("int");

                    b.Property<int?>("TB_LoversId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TB_BlogsId");

                    b.HasIndex("TB_LoversId");

                    b.ToTable("TB_FilePaths");
                });

            modelBuilder.Entity("ourStory.Models.TB_Lovers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.ToTable("TB_Lovers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Semsem",
                            Password = "SemsemFallInLoveWithHisBascota",
                            Role = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bascota",
                            Password = "BascotaFallInLoveWithHerSemsem",
                            Role = "User"
                        },
                        new
                        {
                            Id = 3,
                            Name = "HeroSuperAdmin",
                            Password = "HeroWhatIsHero",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("ourStory.Models.TB_Blogs", b =>
                {
                    b.HasOne("ourStory.Models.Lookups.LK_BlogTypeLookup", "LK_Blog_Type")
                        .WithMany()
                        .HasForeignKey("ID_Blog_Type_LK");

                    b.HasOne("ourStory.Models.Lookups.LK_EventsLookup", "LK_Events")
                        .WithMany()
                        .HasForeignKey("ID_Events_LK");

                    b.HasOne("ourStory.Models.TB_Lovers", "TB_Lovers")
                        .WithMany()
                        .HasForeignKey("ID_Lovers_TB");

                    b.HasOne("ourStory.Models.Lookups.LK_PublishedLookup", "LK_Published")
                        .WithMany()
                        .HasForeignKey("ID_Published_LK");

                    b.Navigation("LK_Blog_Type");

                    b.Navigation("LK_Events");

                    b.Navigation("LK_Published");

                    b.Navigation("TB_Lovers");
                });

            modelBuilder.Entity("ourStory.Models.TB_Descriptions", b =>
                {
                    b.HasOne("ourStory.Models.TB_Lovers", null)
                        .WithMany("TB_Description")
                        .HasForeignKey("TB_LoversId");
                });

            modelBuilder.Entity("ourStory.Models.TB_FilePaths", b =>
                {
                    b.HasOne("ourStory.Models.TB_Blogs", null)
                        .WithMany("TB_FilesPath")
                        .HasForeignKey("TB_BlogsId");

                    b.HasOne("ourStory.Models.TB_Lovers", null)
                        .WithMany("TB_FilesPath")
                        .HasForeignKey("TB_LoversId");
                });

            modelBuilder.Entity("ourStory.Models.TB_Blogs", b =>
                {
                    b.Navigation("TB_FilesPath");
                });

            modelBuilder.Entity("ourStory.Models.TB_Lovers", b =>
                {
                    b.Navigation("TB_Description");

                    b.Navigation("TB_FilesPath");
                });
#pragma warning restore 612, 618
        }
    }
}
