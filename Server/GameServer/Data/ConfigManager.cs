using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameServer
{
	[Serializable]
	public class ServerConfig
	{
		public string dataPath;
		public string ip;
		public int port;
		public string connectionString;

		// 네트워크/세션 유휴 정리(초)
		// - idleTimeoutSeconds: 로비 등 유휴 상태에서 허용할 최대 무수신 시간
		// - inRoomTimeoutSeconds: 룸(매치) 중 허용할 최대 무수신 시간(상대 강종/네트워크 단절 정리 목적)
		public int idleTimeoutSeconds = 300;      // 5분
		public int inRoomTimeoutSeconds = 30;     // 30초
	}

	public class ConfigManager
	{
		public static ServerConfig Config { get; private set; }

		public static void LoadConfig(string path = "./config.json")
		{
			string text = File.ReadAllText(path);
			Config = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerConfig>(text);
		}
	}
}
