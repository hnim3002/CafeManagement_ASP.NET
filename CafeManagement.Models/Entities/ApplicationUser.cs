using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }

        public Guid CafeId { get; set; }
        [ForeignKey("CafeId")]
        public Cafe Cafe { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<WorkSchedules> WorkSchedules { get; set; }
    }
}

namespace CafeManagement.Models
{
    public enum Gender
    {
        Male, Female, Other
    }
}