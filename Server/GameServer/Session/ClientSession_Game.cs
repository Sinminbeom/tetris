using GameServer;
using Google.Protobuf.Protocol;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Data;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                Player = player;
            }
            else
            {
                signUpRes.Result = ESignUpResult.FailDuplicateName;
            }

            Send(signUpRes);
        }

        public void HandleLonInReq(C_LogInReq logInReq)
        {
            S_LogInRes logInRes = new S_LogInRes();
            logInRes.Result = ELogInResult.Success;

            PlayerDb playerDb = DBManager.LogInPlayer(logInReq);

            if (playerDb == null)
            {
                logInRes.Result = ELogInResult.FailIncorrectPassword;
            }
            else
            {
                Player = MakePlayerFromPlayerDb(playerDb);
                logInRes.PlayerInfo = Player.PlayerInfo;
            }

            Send(logInRes);
        }

        public void HandleRoomListReq(C_RoomListReq roomListReq)
        {
            S_RoomListRes roomListRes = new S_RoomListRes();

            List<GameRoom> rooms = GameLogic.Instance.GameRooms;

            Console.WriteLine($"rooms.Count = {rooms.Count}");

            foreach (GameRoom room in rooms)
            {
                RoomInfo roomInfo = new RoomInfo();
                roomInfo.Name = room.Name;
                roomInfo.RoomId = room.RoomId;
                roomInfo.Status = room.State;
                roomInfo.PlayerCount = room.PlayerCount;
                roomListRes.Rooms.Add(roomInfo);
            }

            Send(roomListRes);
        }

        public void HandleCreateRoomReq(C_CreateRoomReq createRoomReq)
        {

            GameLogic.Instance.Push(() => {
                S_CreateRoomRes createRoomRes = new S_CreateRoomRes();

                GameRoom room = GameLogic.Instance.GameRooms.FirstOrDefault(r => r.Name == createRoomReq.Name);

                if (room == null)
                {
                    GameLogic.Instance.Add(Player, createRoomReq.Name);
                    createRoomRes.Result = ECreateRoomResult.Success;
                }
                else
                {
                    createRoomRes.Result = ECreateRoomResult.FailDuplicateName;
                }

                Send(createRoomRes);

                //GameRoom room = GameLogic.Instance.GetRooms().FirstOrDefault(r => r.Name == createRoomReq.Name);

                //if (room == null)
                //{
                //    int roomId = GameLogic.Instance.Add(Player, createRoomReq.Name);
                //    createRoomRes.RoomId = roomId;
                //    createRoomRes.Result = ECreateRoomResult.Success;
                //}
                //else
                //{
                //    createRoomRes.Result = ECreateRoomResult.FailDuplicateName;
                //}

                //Send(createRoomRes);
            });

            //if (room == null)
            //{
            //    int roomId = GameLogic.Instance.Add();
            //    createRoomRes.RoomId = roomId;
            //    createRoomRes.Result = ECreateRoomResult.Success;
            //}
            //else
            //{
            //    createRoomRes.Result = ECreateRoomResult.FailDuplicateName;
            //}

            //Send(createRoomRes);
        }

        public void HandleDeleteRoomReq(C_DeleteRoomReq deleteRoomReq)
        {
            S_DeleteRoomRes deleteRoomRes = new S_DeleteRoomRes();

            // TODO : 사람이 들어가 잇으면 안되고 게임중이면 안되고
            GameLogic.Instance.Remove(deleteRoomReq.RoomIndex);

            deleteRoomRes.Result = EDeleteRoomResult.Success;

            Send(deleteRoomRes);
        }

        public void HandleEnterGame(C_EnterGame enterGamePacket)
        {
            GameRoom gameRoom = GameLogic.Instance.Find(enterGamePacket.RoomIndex);
            gameRoom.EnterGame(Player);

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            S_EnterGame enterGame = new S_EnterGame();

            if (otherPlayer != null)
            {
                enterGame.PlayerInfos.Add(otherPlayer.PlayerInfo);
            }
            //enterGame.RoomIndex = gameRoom.RoomId;
            Send(enterGame);
        }

        Player MakePlayerFromPlayerDb(PlayerDb playerDb)
        {
            Player player = new Player();
            {
                player.PlayerId = playerDb.PlayerDbId;
                player.Name = playerDb.Name;
                player.Email = playerDb.Email;
                player.Session = this;
                player.PlayerInfo.Name = playerDb.Name;
                player.PlayerInfo.PlayerId = playerDb.PlayerDbId;
            }

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
