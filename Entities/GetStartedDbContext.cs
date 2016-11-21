using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Entities
{
    public class GetStartedDbContext : DbContext
    {
        public GetStartedDbContext(DbContextOptions<GetStartedDbContext> options)
            :base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
