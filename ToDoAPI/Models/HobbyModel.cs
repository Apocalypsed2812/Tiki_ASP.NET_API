using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class HobbyModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
