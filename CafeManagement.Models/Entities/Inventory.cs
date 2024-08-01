using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class Inventory
    {
       
        public int ProductId { get; set; }

        public Guid CafeId { get; set; }

        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        public Product Product { get; set; }

        public Cafe Cafe { get; set; }
    }
}
