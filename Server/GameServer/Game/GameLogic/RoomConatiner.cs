using Google.Protobuf.Protocol;

namespace GameServer
{
    public class RoomConatiner
    {
        int _roomId = 1;

        List<GameRoom> _roomList = new List<GameRoom>();
        Dictionary<int, GameRoom> _roomDict = new Dictionary<int, GameRoom>();

        public RoomInfo Add(Player player, string name)
        {
            GameRoom gameRoom = new GameRoom();
            gameRoom.RoomInfo.Name = name;
            gameRoom.RoomInfo.RoomId = _roomId;
            gameRoom.RoomInfo.Status = ERoomState.Waiting;

            gameRoom.Push(gameRoom.Init);

            gameRoom.EnterGame(player);

            _roomList.Add(gameRoom);
            _roomDict.Add(_roomId, gameRoom);

            _roomId++;

            return gameRoom.RoomInfo;
        }

        public bool Remove(int roomId)
        {
            if (_roomDict.TryGetValue(roomId, out GameRoom gameRoom))
            {
                _roomList.Remove(gameRoom);
                _roomDict.Remove(roomId);
                return true;
            }
            return false;
        }

        public GameRoom FindByIndex(int index)
        {
            if (index >= 0 && index < _roomList.Count)
                return _roomList[index];
            return null;
        }

        public GameRoom FindByRoomId(int roomId)
        {
            _roomDict.TryGetValue(roomId, out var room);
            return room;
        }

        public List<GameRoom> ToList()
        {
            return _roomList;
        }
    }
}
