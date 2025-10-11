using Google.Protobuf.Protocol;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GameServer
{
	// GameLogic
	// - GameRoom
	public class GameLogic : JobSerializer
	{
		public static GameLogic Instance { get; } = new GameLogic();

		public List<GameRoom> GameRooms { get { return roomConatiner.ToList(); } }

        public RoomConatiner roomConatiner = new RoomConatiner();

		public void Update()
		{
			Flush();

			foreach (GameRoom room in GameRooms)
			{
				room.Update();
			}
		}

		public RoomInfo Add(Player player, string name)
		{
            return roomConatiner.Add(player, name);
        }

        public bool Remove(int roomId)
        {
            return roomConatiner.Remove(roomId);
        }

        public GameRoom FindByIndex(int index)
        {
            return roomConatiner.FindByIndex(index);
        }

        public GameRoom FindByRoomId(int roomId)
        {
            return roomConatiner.FindByRoomId(roomId);
        }

    }
}
