using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Movies4U.Models;

namespace Movies4U.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180918122908_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Movies4U.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Movies4U.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Movies4U.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MinimumAge");

                    b.Property<string>("Name");

                    b.Property<int>("NumOfMinutes");

                    b.Property<string>("Summary");

                    b.Property<string>("TrailerURL");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("Movies4U.Models.MovieGenres", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("GenreId");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("Movies4U.Models.MovieLanguages", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("LanguageId");

                    b.HasKey("MovieId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("MovieLanguages");
                });

            modelBuilder.Entity("Movies4U.Models.Theator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Theator");
                });

            modelBuilder.Entity("Movies4U.Models.Users", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Movies4U.Models.MovieGenres", b =>
                {
                    b.HasOne("Movies4U.Models.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Movies4U.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Movies4U.Models.MovieLanguages", b =>
                {
                    b.HasOne("Movies4U.Models.Language", "Language")
                        .WithMany("MovieLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Movies4U.Models.Movie", "Movie")
                        .WithMany("MovieLanguages")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
