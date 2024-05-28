using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.Repository.IRepository;
using System.Linq.Expressions;

namespace BookStore.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        
        void ICategoryRepository.Save()
        {
            _db.SaveChanges();
        }

        void ICategoryRepository.Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
