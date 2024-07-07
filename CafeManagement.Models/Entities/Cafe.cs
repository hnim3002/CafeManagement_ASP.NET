using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class Cafe
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;

        public ICollection<ApplicationUser> Employees { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<WorkSchedules> WorkSchedules { get; set; }


    }
}
