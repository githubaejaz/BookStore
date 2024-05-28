using BookStore.Models;

namespace BookStore.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        void Save();
    }
}
