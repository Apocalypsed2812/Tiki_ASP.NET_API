using ToDoAPI.Data;

namespace ToDoAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int ProductCartId { get; set; } // Thêm thuộc tính ProductCartId
        public UserCart ProductCart { get; set; } // Thêm navigation property để EF Core có thể load thông tin ProductCart
    }
}
