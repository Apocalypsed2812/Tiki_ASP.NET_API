using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Specifications;

namespace ToDoAPI.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HobbyContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(HobbyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddCategoryAsync(CategoryModel model)
        {
            var newCategory = _mapper.Map<Category>(model);
            _context.Categories!.Add(newCategory);
            await _context.SaveChangesAsync();

            return newCategory.Id;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var deleteCategory = _context.Categories!.SingleOrDefault(b => b.Id == id);
            if (deleteCategory != null)
            {
                _context.Categories!.Remove(deleteCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync()
        {
            var categories = await _context.Categories!.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }

        public async Task<CategoryModel> GetCategoryAsync(int id)
        {
            var category = await _context.Categories!.FindAsync(id);
            return _mapper.Map<CategoryModel>(category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryModel model)
        {
            if (id == model.Id)
            {
                var updateCategory = _mapper.Map<Category>(model);
                _context.Categories!.Update(updateCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category?> GetCategoryByName(Specification<Category> specifications)
        {
            var categorys = await _context.Categories!.ToListAsync();
            var categoryFilter = categorys.FirstOrDefault(specifications.ToExpression().Compile());
            return categoryFilter;
        }
    }
}
