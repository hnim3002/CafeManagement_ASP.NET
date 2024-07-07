using CafeManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface ICafeRepository : IRepository<Cafe>
    {
        void Update(Cafe obj);
    }
}
