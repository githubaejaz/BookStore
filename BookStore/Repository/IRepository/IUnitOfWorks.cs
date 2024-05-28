namespace BookStore.Repository.IRepository
{
    public interface IUnitOfWorks
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
