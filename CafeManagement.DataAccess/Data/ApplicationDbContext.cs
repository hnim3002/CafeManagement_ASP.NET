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
        }


        public override int SaveChanges()
        {
            // Get the entries that are being added or modified
            var addedOrModifiedReceiptDetails = ChangeTracker.Entries<ReceiptDetail>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            // Iterate through each modified receipt detail
            foreach (var entry in addedOrModifiedReceiptDetails)
            {
                var receiptDetail = entry.Entity;

                // Fetch the related receipt
                var receipt = Receipts
                    .Include(r => r.ReceiptDetails)
                    .FirstOrDefault(r => r.Id == receiptDetail.ReceiptId);

                if (receipt != null)
                {
                    // Calculate the total from all ReceiptDetails related to this receipt
                    receipt.Total = receipt.ReceiptDetails.Sum(rd => rd.Quantity *
                        Inventories.FirstOrDefault(inv => inv.ProductId == rd.ProductId && inv.CafeId == rd.CafeId)?.Price ?? 0);

                    // Update Discount, Tax, and FinalTotal based on your business logic
                    receipt.Discount = receipt.Total * 0.1; // Example 10% discount
                    receipt.Tax = (receipt.Total - receipt.Discount) * 0.15; // Example 15% tax
                    receipt.FinalTotal = (receipt.Total - receipt.Discount) + receipt.Tax;

                    // Mark the receipt as modified to update the values in the database
                    Entry(receipt).State = EntityState.Modified;
                }
            }

            return base.SaveChanges();
        }

    }
}
