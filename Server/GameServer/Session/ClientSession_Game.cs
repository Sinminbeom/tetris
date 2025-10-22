using GameServer;
using Google.Protobuf.Protocol;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Data;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public void HandleLogInReq(C_LogInReq logInReq)
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

            foreach (GameRoom room in rooms)
            {
                roomListRes.Rooms.Add(room.RoomInfo);
            }

            Send(roomListRes);
        }

        public void HandleCreateRoomReq(C_CreateRoomReq createRoomReq)
        {

            GameLogic.Instance.Push(() => {
                S_CreateRoomRes createRoomRes = new S_CreateRoomRes();

                GameRoom room = GameLogic.Instance.GameRooms.FirstOrDefault(r => r.RoomInfo.Name == createRoomReq.Name);

                if (room == null)
                {
                    RoomInfo roomInfo = GameLogic.Instance.Add(Player, createRoomReq.Name);
                    createRoomRes.RoomInfo = roomInfo;
                    createRoomRes.Result = ECreateRoomResult.Success;
                }
                else
                {
                    createRoomRes.Result = ECreateRoomResult.FailDuplicateName;
                }

                Send(createRoomRes);
            });
        }

        public void HandleEnterGame(C_EnterGame enterGamePacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByIndex(enterGamePacket.RoomIndex);

            if (gameRoom == null)
                return;

            gameRoom.EnterGame(Player);

            S_EnterGame enterGame = new S_EnterGame();
            enterGame.RoomInfo = gameRoom.RoomInfo;

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            if (otherPlayer != null)
            {
                enterGame.PlayerInfos.Add(otherPlayer.PlayerInfo);

                S_JoinGame joinGame = new S_JoinGame();
                joinGame.PlayerInfo = Player.PlayerInfo;
                otherPlayer.Session.Send(joinGame);
            }

            Send(enterGame);
        }

        public void HandleLeaveGame(C_LeaveGame leaveGamePacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByIndex(leaveGamePacket.RoomIndex);

            if (gameRoom == null)
                return;

            gameRoom.LeaveGame(Player);

            // 나 자신
            S_LeaveGame leaveGame = new S_LeaveGame();
            Send(leaveGame);

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            //if (otherPlayer == null)
            //    return;

            if (otherPlayer == null)
            {
                GameLogic.Instance.Remove(gameRoom.RoomInfo.RoomId);
                return;
            }

            // 상대방
            S_LeavePlayer leavePlayer = new S_LeavePlayer();
            otherPlayer.Session.Send(leavePlayer);
        }

        public void HandlePlayerState(C_PlayerState playerStatePacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByRoomId(Player.Room.RoomInfo.RoomId);

            if (gameRoom == null)
                return;

            Player.PlayerInfo = playerStatePacket.PlayerInfo;

            // 상대방 패킷 전송
            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            if (otherPlayer == null)
                return;

            S_PlayerState playerState = new S_PlayerState();
            playerState.PlayerInfo = playerStatePacket.PlayerInfo;
            otherPlayer.Session.Send(playerState);

            gameRoom.CheckAllReady();
        }

        public void HandleSpawnTetromino(C_SpawnTetromino spawnTetrominoPacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByRoomId(Player.Room.RoomInfo.RoomId);

            if (gameRoom == null)
                return;

            Player.Board.Tetromino.type = spawnTetrominoPacket.TetrominoType;

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            if (otherPlayer == null)
                return;

            S_SpawnTetromino spawn = new S_SpawnTetromino();
            spawn.TetrominoType = spawnTetrominoPacket.TetrominoType;

            otherPlayer.Session.Send(spawn);
        }

        public void HandleMoveTetromino(C_MoveTetromino moveTetrominoPacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByRoomId(Player.Room.RoomInfo.RoomId);

            if (gameRoom == null)
                return;

            Player.Board.Tetromino.positionInfo = moveTetrominoPacket.PositionInfo;

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            if (otherPlayer == null)
                return;

            S_MoveTetromino moveTetromino = new S_MoveTetromino();
            moveTetromino.PositionInfo = Player.Board.Tetromino.positionInfo;
            otherPlayer.Session.Send(moveTetromino);
        }

        public void HandleLockBlock(C_LockBlock lockBlockPacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByRoomId(Player.Room.RoomInfo.RoomId);

            if (gameRoom == null)
                return;

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            if (otherPlayer == null)
                return;

            S_LockBlock lockBlock = new S_LockBlock();
            otherPlayer.Session.Send(lockBlock);
        }

        public void HandleClearRows(C_ClearRows clearRowsPacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByRoomId(Player.Room.RoomInfo.RoomId);

            if (gameRoom == null)
                return;

            Player otherPlayer = gameRoom.GetOtherPlayer(Player);

            if (otherPlayer == null)
                return;

            S_ClearRows clearRows = new S_ClearRows();
            clearRows.Rows.AddRange(clearRowsPacket.Rows);
            otherPlayer.Session.Send(clearRows);
        }

        public void HandleGameOver(C_GameOver gameOverPacket)
        {
            GameRoom gameRoom = GameLogic.Instance.FindByRoomId(Player.Room.RoomInfo.RoomId);

            if (gameRoom == null)
                return;

            gameRoom.GameOver(Player);
        }

        Player MakePlayerFromPlayerDb(PlayerDb playerDb)
        {
            Player player = new Player();
            {
                player.PlayerInfo.PlayerId = playerDb.PlayerDbId;
                player.PlayerInfo.Name = playerDb.Name;
                player.PlayerInfo.PlayerId = playerDb.PlayerDbId;
                player.PlayerInfo.State = EPlayerState.NotReady;
                player.PlayerInfo.Email = playerDb.Email;
                player.Session = this;
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
