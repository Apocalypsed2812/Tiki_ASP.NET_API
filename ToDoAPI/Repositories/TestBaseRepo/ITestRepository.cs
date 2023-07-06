using ToDoAPI.Data;
using ToDoAPI.Specifications;

namespace ToDoAPI.Repositories.TestBaseRepo
{
    public interface ITestRepository : IBaseRepository<Category>
    {
        public Task<Category> GetCategoryByNameAsync(string categoryName);
    }
}
