using CafeManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.ViewModel
{
    public class InventoryVM
    {
        public IEnumerable<Product> products { get; set; }

        public Inventory inventory { get; set; }
        public IEnumerable<Cafe> cafes { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}
