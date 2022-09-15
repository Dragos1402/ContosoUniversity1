using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantApplication.Models;


namespace RestaurantApplication.Data
{
    public class RestaurantDbContext : DbContext
    {
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) :base(options)  
        {       
        }
         public DbSet<Menu> Menus { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
