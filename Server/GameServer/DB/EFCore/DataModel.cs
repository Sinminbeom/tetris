using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Protobuf.Protocol;

namespace GameServer
{
    [Table("Player")]
    public class PlayerDb
	{
        // Convention : [클래스]Id 으로 명명하면 PK
        public int PlayerDbId { get; set; }
        public DateTime CreateDate { get; private set; }

        public RoomPlayerDb RoomPlayer { get; set; }
    }

    [Table("Room")]
    public class RoomDb
    {
        // Convention : [클래스]Id 으로 명명하면 PK
        public int RoomDbId { get; set; }
        public ERoomState State { get; set; }
        public DateTime CreateDate { get; private set; }

        public List<RoomPlayerDb> RoomPlayers { get; set; } = new();
    }

    [Table("RoomPlayer")]
    public class RoomPlayerDb
    {
        public int RoomPlayerDbId { get; set; }
        public int RoomDbId { get; set; }
        public RoomDb Room {  get; set; }
        public int PlayerDbId { get; set; }
        public PlayerDb Player { get; set; }
        public DateTime CreateDate { get; private set; }
    }
}
