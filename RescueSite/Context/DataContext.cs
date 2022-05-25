using Microsoft.EntityFrameworkCore;
using RescueSite.Entites;

namespace RescueSite.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Winch> Winchs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.con



            base.OnModelCreating(modelBuilder);
        }
    }
}
