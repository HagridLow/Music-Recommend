using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API
{   
    public class DataContext: IdentityDbContext<AppUser>
    {
        public DataContext()
        {
        
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }


        public DbSet<AlbumStatus> AlbumStatuses { get; set; }
        public DbSet<SpotifyAlbumRated> SpotifyAlbumRateds { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.SpotifyAlbumRateds);
            
            modelBuilder.Entity<SpotifyAlbumRated>()
                .HasOne(u => u.User);
                
        }
        


    }
}