using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
