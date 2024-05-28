using BookStore.DataAccess.Data;
using BookStore.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace BookStore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> DbSet;
        public Repository(ApplicationDBContext db)
        {
            _db = db;
            this.DbSet = db.Set<T>();

            //add to show caetgory Name in product index
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }

        void IRepository<T>.Add(T entity)
        {
            DbSet.Add(entity);
        }

        //T IRepository<T>.Get(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = DbSet;
        //    query = query.Where(filter);
        //    return query.FirstOrDefault();
        //}

        //IEnumerable<T> IRepository<T>.GetAll()
        //{
        //    IQueryable<T> query = DbSet;
        //    return query.ToList();
        //}


        //add to show caetgory Name in product index
        T IRepository<T>.Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        //add to show caetgory Name in product index
        IEnumerable<T> IRepository<T>.GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        void IRepository<T>.Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        void IRepository<T>.RemoveRange(IEnumerable<T> entity)
        {
            DbSet.RemoveRange(entity);
        }
    }
}
