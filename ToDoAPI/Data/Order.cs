using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAPI.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        // Mối quan hệ One-to-One với entity Account
        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
