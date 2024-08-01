using CafeManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.ViewModel
{
    public class ViewReceipt
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Phone]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string EmployeeId { get; set; }

        public Cafe Cafe { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public double FinalTotal { get; set; }

    }
}
