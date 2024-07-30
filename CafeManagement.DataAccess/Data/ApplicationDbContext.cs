using CafeManagement.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public DbSet<WorkSchedules> WorkSchedules { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cafe>().HasData(new Cafe {Id= Guid.NewGuid(),  Name = "CafeM1", Address = "96 Dinh cong", });


            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for Inventory
            modelBuilder.Entity<Inventory>()
                .HasKey(i => new { i.ProductId, i.CafeId});

            // Configure relationships
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);

            //modelBuilder.Entity<Inventory>()
            //    .HasOne(i => i.Cafe)
            //    .WithMany()
            //    .HasForeignKey(i => i.CafeId);



            modelBuilder.Entity<ReceiptDetail>()
                .HasKey(rd => new { rd.ProductId, rd.CafeId, rd.ReceiptId });

            // Configure the relationship between ReceiptDetail and Inventory
            modelBuilder.Entity<ReceiptDetail>()
                .HasOne(rd => rd.Inventory)
                .WithMany() // Specify the navigation property in Inventory if there is one
                .HasForeignKey(rd => new { rd.ProductId, rd.CafeId });

            // vxcznship between ReceiptDetail and Receipt

            modelBuilder.Entity<ReceiptDetail>()
                .HasOne(rd => rd.Receipt)
                .WithMany(r => r.ReceiptDetails) // Assuming Receipt has a collection of ReceiptDetails
                .HasForeignKey(rd => rd.ReceiptId);


            modelBuilder.Entity<ReceiptDetail>()
               .HasOne(rd => rd.Receipt)
               .WithMany(r => r.ReceiptDetails)
               .HasForeignKey(rd => rd.ReceiptId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent


            // Configure relationships WorkSchedules

            modelBuilder.Entity<WorkSchedules>()
                 .HasKey(rd => new { rd.Id, rd.CafeId, rd.EmployeeId });


            modelBuilder.Entity<WorkSchedules>()
                .HasOne(rd => rd.Employee)
                .WithMany(r => r.WorkSchedules)
                .HasForeignKey(rd => rd.EmployeeId)

                .OnDelete(DeleteBehavior.Restrict);

         



            // Configure relationships Customer

            modelBuilder.Entity<Customer>()
                    .HasKey(rd => new { rd.Id});

            



        }

    }
}
