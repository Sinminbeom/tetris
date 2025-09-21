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
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; private set; }
    }
}
