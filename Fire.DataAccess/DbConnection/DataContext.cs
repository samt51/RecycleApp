using Fire.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Fire.DataAccess.DbConnection
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\NETAX;Database=FireDb;Trusted_Connection=True");
            //optionsBuilder.UseSqlServer(@"Server=77.245.159.10\MSSQLSERVER2019;Database=ticaretdb;User ID=samet123;Password=1425369As;Trusted_Connection=false;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FactoryProductType>().
HasOne(x => x.factory)
.WithMany(x => x.factoryProductTypes)
.HasForeignKey(x => x.factoryid);

            modelBuilder.Entity<User>().
HasOne(x => x.userRoles)
.WithMany(x => x.Users)
.HasForeignKey(x => x.userroleid);




        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserRoles> userRoles { get; set; }
        public DbSet<ProductQuantity> productQuantity { get; set; }
        public DbSet<FactoryQuantity> factoryQuantities { get; set; }
        public DbSet<Factory> factories { get; set; }
        public DbSet<Earnings> earnings { get; set; }
        public DbSet<StockTracking> StockTracking { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<PaymentCont> PaymentCont { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<bank> Bank { get; set; }
        public DbSet<Check> CheckCont { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
    }
}
