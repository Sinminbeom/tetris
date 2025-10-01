using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Azure.Core.HttpHeader;

namespace GameServer
{
	// GameLogic
	// - GameRoom
	public class GameLogic : JobSerializer
	{
		public static GameLogic Instance { get; } = new GameLogic();

		public List<GameRoom> GameRooms { get { return _rooms; } }

		List<GameRoom> _rooms = new List<GameRoom>();
		int _roomId = 1;

		public void Update()
		{
			Flush();

			foreach (GameRoom room in _rooms)
			{
				room.Update();
			}
		}

		public void Add(Player player, string name)
		{
			GameRoom gameRoom = new GameRoom();
			gameRoom.hostPlayer = player;
			gameRoom.Name = name;
			gameRoom.RoomId = _roomId;

            gameRoom.Push(gameRoom.Init);

            gameRoom.EnterGame(player);

            _rooms.Add(gameRoom);
        }

		public bool Remove(int roomIndex)
		{
			GameRoom gameRoom = _rooms[roomIndex];
			return _rooms.Remove(gameRoom);
		}

		public GameRoom Find(int roomIndex)
		{
            if (roomIndex >= 0 && roomIndex < _rooms.Count)
            {
                return _rooms[roomIndex];
            }
            else
            {
				return null;
            }
		}
	}
}
