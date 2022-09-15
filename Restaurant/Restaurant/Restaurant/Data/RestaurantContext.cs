
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options): base(options)
        {
        }
            public DbSet<Client> Clients { get; set; }
            public DbSet<Menu> Menus { get; set; }
    }
}
