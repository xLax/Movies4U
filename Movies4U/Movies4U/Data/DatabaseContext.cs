using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies4U.Models;

namespace Movies4U.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Define the keys for Many 2 Many connections
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Movie Genres
            modelBuilder.Entity<MovieGenres>()
                .HasKey(t => new { t.MovieId, t.GenreId });

            modelBuilder.Entity<MovieGenres>()
                .HasOne(pt => pt.Movie)
                .WithMany(p => p.MovieGenres)
                .HasForeignKey(pt => pt.MovieId);

            modelBuilder.Entity<MovieGenres>()
                .HasOne(pt => pt.Genre)
                .WithMany(t => t.MovieGenres)
                .HasForeignKey(pt => pt.GenreId);

            // Movie Languages
            modelBuilder.Entity<MovieLanguages>()
                .HasKey(t => new { t.MovieId, t.LanguageId });

            modelBuilder.Entity<MovieLanguages>()
                .HasOne(pt => pt.Movie)
                .WithMany(p => p.MovieLanguages)
                .HasForeignKey(pt => pt.MovieId);

            modelBuilder.Entity<MovieLanguages>()
                .HasOne(pt => pt.Language)
                .WithMany(t => t.MovieLanguages)
                .HasForeignKey(pt => pt.LanguageId);

        }

        public DbSet<Movies4U.Models.Theator> Theator { get; set; }

        public DbSet<Movies4U.Models.Genre> Genre { get; set; }

        public DbSet<Movies4U.Models.Language> Language { get; set; }

        public DbSet<Movies4U.Models.Movie> Movie { get; set; }

        public DbSet<Movies4U.Models.MovieGenres> MovieGenres { get; set; }

        public DbSet<Movies4U.Models.MovieLanguages> MovieLanguages { get; set; }

        public DbSet<Movies4U.Models.Users> Users { get; set; }
    }
}
