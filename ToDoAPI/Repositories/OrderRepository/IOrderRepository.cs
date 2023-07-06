using ToDoAPI.Data;
using ToDoAPI.DTO;
using ToDoAPI.Models;

namespace ToDoAPI.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> GetAllOrdersAsync();
        public Task AddOrderAsync(int accountId, List<ProductModel> products);
    }
}
