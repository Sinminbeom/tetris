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
	C_CreateRoomReq = 6,
	S_CreateRoomRes = 7,
	C_RoomListReq = 8,
	S_RoomListRes = 9,
	C_EnterRoom = 10,
	S_EnterRoom = 11,
	S_JoinRoom = 12,
	C_LeaveRoom = 13,
	S_LeaveRoom = 14,
	S_LeavePlayer = 15,
	C_PlayerState = 16,
	S_PlayerState = 17,
	S_StartGame = 18,
	C_SpawnTetromino = 19,
	S_SpawnTetromino = 20,
	C_MoveTetromino = 21,
	S_MoveTetromino = 22,
	C_LockBlock = 23,
	S_LockBlock = 24,
	C_ClearRows = 25,
	S_ClearRows = 26,
	C_GameOver = 27,
	S_GameOver = 28,
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
		_onRecv.Add((ushort)MsgId.C_SignUpReq, MakePacket<C_SignUpReq>);
		_handler.Add((ushort)MsgId.C_SignUpReq, PacketHandler.C_SignUpReqHandler);		
		_onRecv.Add((ushort)MsgId.C_LogInReq, MakePacket<C_LogInReq>);
		_handler.Add((ushort)MsgId.C_LogInReq, PacketHandler.C_LogInReqHandler);		
		_onRecv.Add((ushort)MsgId.C_CreateRoomReq, MakePacket<C_CreateRoomReq>);
		_handler.Add((ushort)MsgId.C_CreateRoomReq, PacketHandler.C_CreateRoomReqHandler);		
		_onRecv.Add((ushort)MsgId.C_RoomListReq, MakePacket<C_RoomListReq>);
		_handler.Add((ushort)MsgId.C_RoomListReq, PacketHandler.C_RoomListReqHandler);		
		_onRecv.Add((ushort)MsgId.C_EnterRoom, MakePacket<C_EnterRoom>);
		_handler.Add((ushort)MsgId.C_EnterRoom, PacketHandler.C_EnterRoomHandler);		
		_onRecv.Add((ushort)MsgId.C_LeaveRoom, MakePacket<C_LeaveRoom>);
		_handler.Add((ushort)MsgId.C_LeaveRoom, PacketHandler.C_LeaveRoomHandler);		
		_onRecv.Add((ushort)MsgId.C_PlayerState, MakePacket<C_PlayerState>);
		_handler.Add((ushort)MsgId.C_PlayerState, PacketHandler.C_PlayerStateHandler);		
		_onRecv.Add((ushort)MsgId.C_SpawnTetromino, MakePacket<C_SpawnTetromino>);
		_handler.Add((ushort)MsgId.C_SpawnTetromino, PacketHandler.C_SpawnTetrominoHandler);		
		_onRecv.Add((ushort)MsgId.C_MoveTetromino, MakePacket<C_MoveTetromino>);
		_handler.Add((ushort)MsgId.C_MoveTetromino, PacketHandler.C_MoveTetrominoHandler);		
		_onRecv.Add((ushort)MsgId.C_LockBlock, MakePacket<C_LockBlock>);
		_handler.Add((ushort)MsgId.C_LockBlock, PacketHandler.C_LockBlockHandler);		
		_onRecv.Add((ushort)MsgId.C_ClearRows, MakePacket<C_ClearRows>);
		_handler.Add((ushort)MsgId.C_ClearRows, PacketHandler.C_ClearRowsHandler);		
		_onRecv.Add((ushort)MsgId.C_GameOver, MakePacket<C_GameOver>);
		_handler.Add((ushort)MsgId.C_GameOver, PacketHandler.C_GameOverHandler);
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