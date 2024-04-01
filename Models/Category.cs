using System.ComponentModel.DataAnnotations;

namespace Gaming.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
