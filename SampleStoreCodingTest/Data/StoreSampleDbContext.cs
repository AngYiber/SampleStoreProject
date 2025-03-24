using Microsoft.EntityFrameworkCore;
using SampleStoreCodingTest.Models.Dtos;
using SampleStoreCodingTest.Models.Entities;


namespace SampleStoreCodingTest.Data
{
    public class StoreSampleDbContext : DbContext
    {
        public StoreSampleDbContext(DbContextOptions<StoreSampleDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Shipper> Shippers => Set<Shipper>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<CustomerOrderPredictionDto> CustomerOrderPredictions => Set<CustomerOrderPredictionDto>();
        public DbSet<CustomerOrderDto> CustomerOrders => Set<CustomerOrderDto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees", schema: "HR");
            modelBuilder.Entity<Customer>().ToTable("Customers", "Sales");
            modelBuilder.Entity<Order>().ToTable("Orders", "Sales");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails", "Sales");
            modelBuilder.Entity<Shipper>().ToTable("Shippers", "Sales");
            modelBuilder.Entity<Product>().ToTable("Products", "Production");
            modelBuilder.Entity<Category>().ToTable("Categories", "Production");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers", "Production");

            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });
            modelBuilder.Entity<Customer>().HasKey(c => c.CustId);
            modelBuilder.Entity<Employee>().HasKey(e => e.EmpId);
            modelBuilder.Entity<CustomerOrderPredictionDto>().HasNoKey();
            modelBuilder.Entity<CustomerOrderDto>().HasNoKey();
        }
    }
}
