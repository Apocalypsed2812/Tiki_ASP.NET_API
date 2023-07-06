namespace ToDoAPI.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserCartDTO UserCart { get; set; }
    }
}
