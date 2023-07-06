namespace ToDoAPI.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public CategoryDTO Category { get; set; }
        public List<UserCartProductDTO> UserCartProducts { get; set; }
    }
}
