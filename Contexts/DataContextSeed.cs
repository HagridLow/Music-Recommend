using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;

namespace API.Contexts
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {
            try 
            {
                if(!context.AlbumRatings.Any())
                {
                    var ratingsData = File.ReadAllText("SeedData/rating.json");

                    var ratings = JsonSerializer.Deserialize<List<AlbumRating>>(ratingsData);

                    foreach(var item in ratings)
                    {
                        context.AlbumRatings.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.AlbumStatuses.Any())
                {
                    var statusData = File.ReadAllText("SeedData/listeningstatus.json");

                    var status = JsonSerializer.Deserialize<List<AlbumStatus>>(statusData);

                    foreach(var item in status)
                    {
                        context.AlbumStatuses.Add(item);
                    }
                }

                    await context.SaveChangesAsync();  
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}