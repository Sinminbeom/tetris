namespace GameServer
{
    public class Player
    {
        public string Name { get; set; }

        private Board _board = new Board();
        public Board Board { get; set; }


    }

}