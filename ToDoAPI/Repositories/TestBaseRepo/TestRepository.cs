using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Specifications;

namespace ToDoAPI.Repositories.TestBaseRepo
{
    public class TestRepository : BaseRepository<Category>, ITestRepository
    {
        public TestRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
            
        }
        public Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
