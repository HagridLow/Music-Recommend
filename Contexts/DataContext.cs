using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API
{   
    public class DataContext: DbContext
    {
        public DataContext()
        {
            
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AlbumStatus> AlbumStatuses { get; set; }
        public DbSet<SpotifyAlbum> SpotifyAlbums { get; set; }
        public DbSet<AlbumRaters> AlbumRaters { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AlbumRaters>(x => x.HasKey(aa => new {aa.AppUserId, aa.AlbumId}));

            builder.Entity<AlbumRaters>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.SpotifyAlbumRateds)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<AlbumRaters>()
                .HasOne(u => u.SpotifyAlbumRated)
                .WithMany(a => a.Raters)
                .HasForeignKey(aa => aa.AlbumId);
        }
    }
}