using ToDoAPI.Data;
using ToDoAPI.Models;

namespace ToDoAPI.Services.NewFolder
{
    public interface ITestServices
    {
        public Task<Category> AddAsync(CategoryModel entity);
        public Task DeleteAsync(int id);
        public Task<List<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(int id);
        public Task UpdateAsync(int id, CategoryModel entity);
        public Task Save();
    }
}
