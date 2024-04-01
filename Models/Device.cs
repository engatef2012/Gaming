using System.ComponentModel.DataAnnotations;

namespace Gaming.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Icon { get; set; }
    }
}
