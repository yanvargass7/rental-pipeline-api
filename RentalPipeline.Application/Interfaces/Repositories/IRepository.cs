namespace RentalPipeline.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Update(T entity);
        Task InsertAsync(T entity);
        Task<T?> GetByIdAsync(int id);
    }
}
