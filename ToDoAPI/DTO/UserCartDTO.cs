namespace ToDoAPI.DTO
{
    public class UserCartDTO
    {
        public int Id { get; set; }
        public List<UserCartProductDTO> UserCartProducts { get; set; }
    }
}
