using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICafeRepository Cafe {  get; }
    
        Task SaveAsync();
    }
}
