namespace GameServer
{
    public class Player
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public Board Board { get; set; } = new Board();
    }

}