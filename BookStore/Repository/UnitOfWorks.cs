using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly ApplicationDBContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitOfWorks(ApplicationDBContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
