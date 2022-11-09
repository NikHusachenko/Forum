using Microsoft.EntityFrameworkCore;

namespace Forum.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IQueryable<T> Table { get; set; }

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Table = _applicationDbContext.Set<T>();
        }

        public async Task Add(T value)
        {
            await _applicationDbContext.AddAsync(value);
        }

        public async void Delete(T value)
        {
            _applicationDbContext.Remove(value);
        }

        public void Update(T value)
        {
            _applicationDbContext.Update(value);
        }

        public async Task SaveChanges()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}