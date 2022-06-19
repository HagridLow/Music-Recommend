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

        public DbSet<AlbumRating> AlbumRatings { get; set; }
        public DbSet<AlbumStatus> AlbumStatuses { get; set; }
        public DbSet<SpotifyAlbum> SpotifyAlbums { get; set; }



    }
}