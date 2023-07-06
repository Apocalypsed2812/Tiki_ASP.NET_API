namespace ToDoAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; }
    }
}
