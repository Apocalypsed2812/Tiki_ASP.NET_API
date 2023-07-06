namespace ToDoAPI.Data
{
    public class UserCart
    {
        public int Id { get; set; }
        public List<UserCartProduct> UserCartProducts { get; set; }

        // Mối quan hệ One-to-One với entity Account
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
