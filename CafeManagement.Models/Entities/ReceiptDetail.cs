using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models.Entities
{
    public class ReceiptDetail
    {
        
        public Guid ReceiptId { get; set; }

        public int ProductId { get; set; }

        public Guid CafeId { get; set; }

        public int Quantity { get; set; }

        public Receipt Receipt { get; set; }
        public Inventory Inventory { get; set; }
    }
}
