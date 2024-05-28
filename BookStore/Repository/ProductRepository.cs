using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.Repository.IRepository;

namespace BookStore.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _db;
        public ProductRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        void IProductRepository.Save()
        {
            _db.SaveChanges();
        }

        void IProductRepository.Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
