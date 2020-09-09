﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planter_API_2.Models;

namespace Planter_API_2.Migrations
{
    [DbContext(typeof(PlantsContext))]
    partial class PlantsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Planter_API_2.Models.ApprovedType", b =>
                {
                    b.Property<int>("ApprovedTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApprovedTypeID");

                    b.ToTable("ApprovedTypes");
                });

            modelBuilder.Entity("Planter_API_2.Models.Article", b =>
                {
                    b.Property<int>("ArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApprovedTypeID")
                        .HasColumnType("int");

                    b.Property<int>("PlantsID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tips")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleID");

                    b.HasIndex("ApprovedTypeID");

                    b.HasIndex("PlantsID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Planter_API_2.Models.Climates", b =>
                {
                    b.Property<int>("ClimateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Climate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClimateID");

                    b.ToTable("Climates");
                });

            modelBuilder.Entity("Planter_API_2.Models.Comments", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FK_ArticleID")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentID");

                    b.HasIndex("FK_ArticleID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Planter_API_2.Models.Edible", b =>
                {
                    b.Property<int>("EdibleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EdibleS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EdibleID");

                    b.ToTable("Edibles");
                });

            modelBuilder.Entity("Planter_API_2.Models.PlantType", b =>
                {
                    b.Property<int>("PlantTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlantTypeID");

                    b.ToTable("PlantTypes");
                });

            modelBuilder.Entity("Planter_API_2.Models.Plants", b =>
                {
                    b.Property<int>("PlantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FK_ApprovedTypeID")
                        .HasColumnType("int");

                    b.Property<int>("FK_ClimateID")
                        .HasColumnType("int");

                    b.Property<int>("FK_EdibleID")
                        .HasColumnType("int");

                    b.Property<int>("FK_PlantTypeID")
                        .HasColumnType("int");

                    b.Property<int>("FK_UserID")
                        .HasColumnType("int");

                    b.Property<string>("PlantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlantID");

                    b.HasIndex("FK_ApprovedTypeID");

                    b.HasIndex("FK_ClimateID");

                    b.HasIndex("FK_EdibleID");

                    b.HasIndex("FK_PlantTypeID");

                    b.HasIndex("FK_UserID");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Planter_API_2.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeID");

                    b.ToTable("Usertypes");
                });

            modelBuilder.Entity("Planter_API_2.Models.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FK_UserTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("FK_UserTypeID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Planter_API_2.Models.Article", b =>
                {
                    b.HasOne("Planter_API_2.Models.ApprovedType", "ApprovedType")
                        .WithMany("Article")
                        .HasForeignKey("ApprovedTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planter_API_2.Models.Plants", "Plants")
                        .WithMany("Articles")
                        .HasForeignKey("PlantsID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Planter_API_2.Models.Comments", b =>
                {
                    b.HasOne("Planter_API_2.Models.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("FK_ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Planter_API_2.Models.Plants", b =>
                {
                    b.HasOne("Planter_API_2.Models.ApprovedType", "ApprovedType")
                        .WithMany("Plants")
                        .HasForeignKey("FK_ApprovedTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planter_API_2.Models.Climates", "Climates")
                        .WithMany("Plants")
                        .HasForeignKey("FK_ClimateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planter_API_2.Models.Edible", "Edible")
                        .WithMany("Plants")
                        .HasForeignKey("FK_EdibleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planter_API_2.Models.PlantType", "PlantType")
                        .WithMany("Plants")
                        .HasForeignKey("FK_PlantTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planter_API_2.Models.Users", "Users")
                        .WithMany("Plants")
                        .HasForeignKey("FK_UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Planter_API_2.Models.Users", b =>
                {
                    b.HasOne("Planter_API_2.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("FK_UserTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
