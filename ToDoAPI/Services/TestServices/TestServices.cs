using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Repositories.TestBaseRepo;

namespace ToDoAPI.Services.NewFolder
{
    public class TestServices : ITestServices
    {
        private readonly ITestRepository _testRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TestServices(ITestRepository testRepo, IUnitOfWork unitOfWork) {
            _testRepo = testRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> AddAsync(CategoryModel entity)
        {
            var newCategory = _unitOfWork.GetMapper().Map<Category>(entity);
            await _testRepo.AddAsync(newCategory);
            var category = _unitOfWork.GetMapper().Map<Category>(newCategory);
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            await _testRepo.DeleteAsync(id);
        }

        public Task<List<Category>> GetAllAsync()
        {
            var categorys = _testRepo.GetAllAsync();
            return categorys;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _testRepo.GetByIdAsync(id);
            return category;
        }

        public async Task Save()
        {
            await _unitOfWork.Commit();
        }

        public async Task UpdateAsync(int id, CategoryModel entity)
        {
            var newCategory = _unitOfWork.GetMapper().Map<Category>(entity);
            await _testRepo.UpdateAsync(id, newCategory);
        }
    }
}
