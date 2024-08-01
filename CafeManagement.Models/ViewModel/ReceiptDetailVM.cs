using CafeManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.ViewModel
{
    public class ReceiptDetailVM
    {
        [Required]
        public Guid ReceiptId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public Guid CafeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Receipt Receipt { get; set; }
        public Inventory Inventory { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> InventoryList { get; set; }

    }
}
