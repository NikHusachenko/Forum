using Microsoft.EntityFrameworkCore;

namespace Forum.EntityFramework.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; set; }

        Task Add(T value);
        void Delete(T value);
        void Update(T value);
        Task SaveChanges();
    }
}