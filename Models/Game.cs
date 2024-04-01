using System.ComponentModel.DataAnnotations;

namespace Gaming.Models
{
    public class Game
    {
        public int GameId { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Cover { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> GameDevices { get; set; } = new List<GameDevice>();
    }
}
