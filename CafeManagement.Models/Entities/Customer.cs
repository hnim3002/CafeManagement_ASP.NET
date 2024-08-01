using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
