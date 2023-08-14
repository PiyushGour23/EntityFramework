using Billboard.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billboard.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext (DbContextOptions<UserDbContext> options) : base(options)     // dbcontextoptions this allow setting some options needed by the dbcontext like the connection string for instance
        {
            
        }
        public DbSet<User> User { get; set; } // Dbset is allow us to manipulate the data from the table issue 

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<usercred> usercred { get; set; }
    }

}
