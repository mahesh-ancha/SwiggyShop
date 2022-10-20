using Microsoft.EntityFrameworkCore;
using Swiggy.Models;

namespace Swiggy.Data
{
    public class SwiggyDbContext : DbContext
    {
        public SwiggyDbContext(DbContextOptions options):base(options)
        {
                
        }
       public  DbSet<OrdersModel> Orders { get; set; }
       public DbSet<CustomerModel> Customers { get; set; }
       public  DbSet<ProductsModel> Products { get; set; }
    }
}
