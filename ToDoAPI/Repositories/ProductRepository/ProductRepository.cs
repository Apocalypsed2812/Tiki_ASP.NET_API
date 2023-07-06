using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Specifications;

namespace ToDoAPI.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly HobbyContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(HobbyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddProductAsync(ProductModel model)
        {
            var newProduct = _mapper.Map<Product>(model);
            //var category = _context.Categories!.SingleOrDefaultAsync(c => c.Id == newProduct.Id);
            //if(category == null)
            //{
               // return -1;
            //}
            _context.Products!.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.Id;
        }

        public async Task DeleteProductAsync(int id)
        {
            var deleteProduct = _context.Products!.SingleOrDefault(p => p.Id == id);
            if (deleteProduct != null)
            {
                _context.Products!.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var products = await _context.Products!.ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product = await _context.Products!.FindAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task UpdateProductAsync(int id, ProductModel model)
        {
            if (id == model.Id)
            {
                var updateProduct = _mapper.Map<Product>(model);
                _context.Products!.Update(updateProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetProđuctGreaterThanPrice(Specification<Product> specification)
        {
            var products = await _context.Products!.ToListAsync();
            var productFilter = products.Where(specification.ToExpression().Compile()).ToList();
            return productFilter;
        }
    }
}
