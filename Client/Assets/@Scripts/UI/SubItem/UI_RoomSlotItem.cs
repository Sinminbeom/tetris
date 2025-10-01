using Google.Protobuf.Protocol;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RoomSlotItem : UI_Base
{
    private enum GameObjects
    {
    }

    private enum Texts
    {
        RoomNameText,
        RoomStatusText,
        PlayerCountText
    }
    
    private enum Images
    {
        RoomFrameImage,
        RoomImage,
        PlayerCountFrameImage,
        SelectRoomImage
    }

    int _index;
    RoomInfo _info;
    bool _selected = false;
	Action<int> _onRoomSelected;

	protected override void Awake()
    {
        base.Awake();
        
        BindObjects(typeof(GameObjects));
        BindTexts(typeof(Texts));    
        BindImages(typeof(Images));

        GetImage((int)Images.SelectRoomImage).gameObject.BindEvent(OnClickSelectHeroImage);
    }

    public void SetInfo(int index, RoomInfo info, bool selected, Action<int> onRoomSelected)
    {
        _index = index;
        _info = info;
        _selected = selected;
        _onRoomSelected = onRoomSelected;
		RefreshUI();
    }

    public void RefreshUI()
    {
        if (_info == null)
            return;

		GetText((int)Texts.RoomNameText).text = _info.Name;
		GetText((int)Texts.RoomStatusText).text = _info.Status.ToString();
        GetText((int)Texts.PlayerCountText).text = _info.PlayerCount.ToString();

		if (_selected)
            GetImage((int)Images.SelectRoomImage).color = new Color(0.8f, 0.8f, 0.15f, 0.15f);
        else
            GetImage((int)Images.SelectRoomImage).color = new Color(0.8f, 0.8f, 0.15f, 0);
	}

    void OnClickSelectHeroImage(PointerEventData evt)
    {
        _onRoomSelected?.Invoke(_index);
		RefreshUI();
	}
}
