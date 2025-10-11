using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;

public enum MsgId
{
	S_Connected = 1,
	C_SignUpReq = 2,
	S_SignUpRes = 3,
	C_LogInReq = 4,
	S_LogInRes = 5,
	C_DeleteRoomReq = 6,
	S_DeleteRoomRes = 7,
	C_CreateRoomReq = 8,
	S_CreateRoomRes = 9,
	C_RoomListReq = 10,
	S_RoomListRes = 11,
	C_EnterGame = 12,
	S_EnterGame = 13,
	S_JoinGame = 14,
	C_PlayerState = 15,
	S_PlayerState = 16,
	S_StartGame = 17,
	C_SpawnTetromino = 18,
	S_SpawnTetromino = 19,
	C_MoveTetromino = 20,
	S_MoveTetromino = 21,
	C_LockBlock = 22,
	S_LockBlock = 23,
	C_ClearRows = 24,
	S_ClearRows = 25,
}

class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
		
	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }

	public void Register()
	{		
		_onRecv.Add((ushort)MsgId.S_Connected, MakePacket<S_Connected>);
		_handler.Add((ushort)MsgId.S_Connected, PacketHandler.S_ConnectedHandler);		
		_onRecv.Add((ushort)MsgId.S_SignUpRes, MakePacket<S_SignUpRes>);
		_handler.Add((ushort)MsgId.S_SignUpRes, PacketHandler.S_SignUpResHandler);		
		_onRecv.Add((ushort)MsgId.S_LogInRes, MakePacket<S_LogInRes>);
		_handler.Add((ushort)MsgId.S_LogInRes, PacketHandler.S_LogInResHandler);		
		_onRecv.Add((ushort)MsgId.S_DeleteRoomRes, MakePacket<S_DeleteRoomRes>);
		_handler.Add((ushort)MsgId.S_DeleteRoomRes, PacketHandler.S_DeleteRoomResHandler);		
		_onRecv.Add((ushort)MsgId.S_CreateRoomRes, MakePacket<S_CreateRoomRes>);
		_handler.Add((ushort)MsgId.S_CreateRoomRes, PacketHandler.S_CreateRoomResHandler);		
		_onRecv.Add((ushort)MsgId.S_RoomListRes, MakePacket<S_RoomListRes>);
		_handler.Add((ushort)MsgId.S_RoomListRes, PacketHandler.S_RoomListResHandler);		
		_onRecv.Add((ushort)MsgId.S_EnterGame, MakePacket<S_EnterGame>);
		_handler.Add((ushort)MsgId.S_EnterGame, PacketHandler.S_EnterGameHandler);		
		_onRecv.Add((ushort)MsgId.S_JoinGame, MakePacket<S_JoinGame>);
		_handler.Add((ushort)MsgId.S_JoinGame, PacketHandler.S_JoinGameHandler);		
		_onRecv.Add((ushort)MsgId.S_PlayerState, MakePacket<S_PlayerState>);
		_handler.Add((ushort)MsgId.S_PlayerState, PacketHandler.S_PlayerStateHandler);		
		_onRecv.Add((ushort)MsgId.S_StartGame, MakePacket<S_StartGame>);
		_handler.Add((ushort)MsgId.S_StartGame, PacketHandler.S_StartGameHandler);		
		_onRecv.Add((ushort)MsgId.S_SpawnTetromino, MakePacket<S_SpawnTetromino>);
		_handler.Add((ushort)MsgId.S_SpawnTetromino, PacketHandler.S_SpawnTetrominoHandler);		
		_onRecv.Add((ushort)MsgId.S_MoveTetromino, MakePacket<S_MoveTetromino>);
		_handler.Add((ushort)MsgId.S_MoveTetromino, PacketHandler.S_MoveTetrominoHandler);		
		_onRecv.Add((ushort)MsgId.S_LockBlock, MakePacket<S_LockBlock>);
		_handler.Add((ushort)MsgId.S_LockBlock, PacketHandler.S_LockBlockHandler);		
		_onRecv.Add((ushort)MsgId.S_ClearRows, MakePacket<S_ClearRows>);
		_handler.Add((ushort)MsgId.S_ClearRows, PacketHandler.S_ClearRowsHandler);
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (_onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

		if (CustomHandler != null)
		{
			CustomHandler.Invoke(session, pkt, id);
		}
		else
		{
			Action<PacketSession, IMessage> action = null;
			if (_handler.TryGetValue(id, out action))
				action.Invoke(session, pkt);
		}
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (_handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}