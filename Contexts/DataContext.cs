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

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected DataContext()
        {
        }

        public DbSet<AlbumStatus> AlbumStatuses { get; set; }
        public DbSet<SpotifyAlbumRated> SpotifyAlbumRateds { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.SpotifyAlbumRateds);
            
            modelBuilder.Entity<SpotifyAlbumRated>()
                .HasOne(u => u.User);
            
            modelBuilder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new {k.ObserverId, k.TargetId});

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                b.HasOne(o => o.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(o => o.TargetId)
                    .OnDelete(DeleteBehavior.Cascade);     
            });
        }
        


    }
}