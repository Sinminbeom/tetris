using Microsoft.EntityFrameworkCore;
using Server.Data;
using GameServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Protobuf.Protocol;

namespace Server
{
	public partial class ClientSession : PacketSession
	{
        public void HandleSignUpReq(C_SignUpReq reqUpReq)
        {
            S_SignUpRes signUpRes = new S_SignUpRes();

            // 1) 이름이 안 겹치는지 확인
            // 2) 생성 진행
            PlayerDb playerDb = DBManager.CreatePlayerDb(reqUpReq);
            if (playerDb != null)
            {
                signUpRes.Result = ESignUpResult.Success;
                // 메모리에 캐싱
                Player player = MakePlayerFromPlayerDb(playerDb);
                Players.Add(player);
            }
            else
            {
                signUpRes.Result = ESignUpResult.FailDuplicateName;
            }

            Send(signUpRes);
        }

        public Player MakePlayerFromPlayerDb(PlayerDb playerDb)
        {
            Player player = new Player()
            {
                Name = playerDb.Name,
                Email = playerDb.Email,
            };

            return player;
        }
        //public void HandleEnterGame(C_EnterGame enterGamePacket)
        //{
        //	// TODO : 인증 토큰 

        //	Console.WriteLine("HandleEnterGame");

        //	//MyHero = ObjectManager.Instance.Spawn<Hero>(1);
        //	//{
        //	//	MyHero.ObjectInfo.PosInfo.State = EObjectState.Idle;
        //	//	MyHero.ObjectInfo.PosInfo.MoveDir = EMoveDir.Down;
        //	//	MyHero.ObjectInfo.PosInfo.PosX = 0;
        //	//	MyHero.ObjectInfo.PosInfo.PosY = 0;
        //	//	MyHero.Session = this;
        //	//}

        //	// TODO : DB에서 마지막 좌표 등 갖고 와서 처리.
        //	GameLogic.Instance.Push(() =>
        //	{
        //		GameRoom room = GameLogic.Instance.Find(1);

        //		//room?.Push(() =>
        //		//{
        //		//	Hero hero = MyHero;
        //		//	room.EnterGame(hero, respawn:false, pos: null);
        //		//});
        //	});
        //}
    }
}
