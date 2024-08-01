using CafeManagement.Models.Entities;

namespace CafeManagement.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
