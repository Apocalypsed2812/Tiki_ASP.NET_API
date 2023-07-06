using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Specifications;

namespace ToDoAPI.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryModel>> GetAllCategoryAsync();
        public Task<CategoryModel> GetCategoryAsync(int id);
        public Task<int> AddCategoryAsync(CategoryModel model);
        public Task UpdateCategoryAsync(int id, CategoryModel model);
        public Task DeleteCategoryAsync(int id);
        public Task<Category?> GetCategoryByName(Specification<Category> specification);
    }
}
