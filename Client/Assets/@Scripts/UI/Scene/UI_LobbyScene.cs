using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_LobbyScene : UI_Scene
{
    enum GameObjects
    {
		RoomList
	}

    enum Buttons
    {
        StartButton,
        CreateRoomButton,
    }

    // 캐릭터 슬롯
    Transform _parent;
    List<UI_RoomSlotItem> _slots = new List<UI_RoomSlotItem>();

	// 데이터
	List<RoomInfo> _rooms = new List<RoomInfo>();

    protected override void Awake()
    { 
        base.Awake();
        
        BindObjects(typeof(GameObjects));
        BindButtons(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.CreateRoomButton).gameObject.BindEvent(OnClickCreateRoomButton);

		_parent = GetObject((int)GameObjects.RoomList).transform;

		SendRoomListReqPacket();
        PopulateSlots();
		RefreshUI();
	}

	public void SetInfo(List<RoomInfo> rooms)
    {
		_rooms = rooms;

		RefreshUI();
    }

	void PopulateSlots()
	{
		_parent.DestroyChildren();
		_slots.Clear();

		for (int i = 0; i < MAX_LOBBY_ROOM_COUNT; i++)
		{
            UI_RoomSlotItem item = Managers.UI.MakeSubItem<UI_RoomSlotItem>(_parent);
			item.gameObject.SetActive(false);
			_slots.Add(item);
		}
	}

	int _selectedRoomIndex = 0;

	public void RefreshUI()
    {
		for (int i = 0; i < MAX_LOBBY_ROOM_COUNT; i++)
		{
			if (i < _rooms.Count)
			{
				RoomInfo roomInfo = _rooms[i];

				_slots[i].SetInfo(i, roomInfo, _selectedRoomIndex == i, OnRoomSelected);
				_slots[i].gameObject.SetActive(true);
			}
			else
			{
				_slots[i].gameObject.SetActive(false);
			}
		}
	}

	void OnRoomSelected(int index)
	{
        _selectedRoomIndex = index;
		RefreshUI();
	}

	void OnClickStartButton(PointerEventData evt)
    {
        // 1) 게임씬 전환
        // 2) C_EnterGame 패킷 전송
        //Managers.Game.SelectedHeroIndex = _selectedHeroIndex;
        //Managers.Scene.LoadScene(EScene.GameScene);

        Managers.Room.SelectedRoomIndex = _selectedRoomIndex;

        UI_RoomPopup roomPopup = Managers.UI.ShowPopupUI<UI_RoomPopup>();
		roomPopup.SetInfo(SendRoomListReqPacket);

        C_EnterRoom enterRoom = new C_EnterRoom();
        enterRoom.RoomIndex = _selectedRoomIndex;
		Managers.Network.Send(enterRoom);
    }

	void OnClickCreateRoomButton(PointerEventData evt)
	{
        // 1) 캐릭터 최대 개수 확인 후, 바로 팝업.
        // 2) UI_CreateCharacterPopup에서 나머지 진행.
        // 3) 캐릭터 생성 팝업 닫힐 때, 캐릭터 목록 다시 요청.
        UI_CreateRoomPopup createRoomPopup = Managers.UI.ShowPopupUI<UI_CreateRoomPopup>();
	}

	public void SendRoomListReqPacket()
	{
		C_RoomListReq reqPacket = new C_RoomListReq();
		Managers.Network.Send(reqPacket);
	}

    public void OnRoomListResHandler(S_RoomListRes resPacket)
    {
        List<RoomInfo> rooms = resPacket.Rooms.ToList();
		SetInfo(rooms);
	}

}
