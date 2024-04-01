namespace Gaming.Models
{
    public class GameDevice
    {
        public int GameId { get; set; }
        public int DeviceId { get; set; }
        public Game Game { get; set; }
        public Device Device { get; set; }
    }
}
