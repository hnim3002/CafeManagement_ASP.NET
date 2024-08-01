using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class Receipt
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Employees { get; set; }
        [Required]
        public Guid CafeId { get; set; }
        [ForeignKey("CafeId")]
        public Cafe Cafe { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public double FinalTotal { get; set; }
        
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
