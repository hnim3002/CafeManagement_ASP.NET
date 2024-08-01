using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICafeRepository Cafe { get; }
        ICategoryRepository Category { get; }
        IInventoryRepository Inventory { get; }
        IProductRepository Product { get; }
        ICustomerRepository Customer { get; }
        IApplicationUserRepository ApplicationUser { get; }

        IReceiptDetailRepository ReceiptDetail { get; }

        IReceiptRepository Receipt { get; }
    
        Task SaveAsync();
    }
}
