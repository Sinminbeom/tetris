using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;
using Object = UnityEngine.Object;

public class UI_TitleScene : UI_Scene
{

    private enum Texts
    {
        StartText,
        StatusText,
    }

    private enum ETitleSceneState
    {
        None,

        // �ּ� �ε�
        AssetLoading,
        AssetLoaded,

        // ���� ����
        LoginSuccess,
        LoginFail,

        // ���� ���� ����
        ConnectingToGameServer,
        ConnectedToGameServer,
        FailedToConnectToGameServer,
    }

    ETitleSceneState _state = ETitleSceneState.None;
    ETitleSceneState State
    {
        get { return _state; }
        set
        {
            Debug.Log($"TitleSceneState : {_state} -> {value}");

            _state = value;

            GetText(((int)Texts.StartText)).gameObject.SetActive(true);

            switch (value)
            {
                case ETitleSceneState.None:
                    break;
                case ETitleSceneState.AssetLoading:
                    GetText((int)Texts.StatusText).text = $"TODO �ε���";
                    GetText(((int)Texts.StartText)).gameObject.SetActive(false);
                    break;
                case ETitleSceneState.AssetLoaded:
                    GetText((int)Texts.StatusText).text = "TODO �α����� ���ּ���";
                    GetText(((int)Texts.StartText)).gameObject.SetActive(false);
                    break;
                case ETitleSceneState.LoginSuccess:
                    GetText((int)Texts.StatusText).text = "TODO �α��� ����!";
                    GetText((int)Texts.StartText).text = "TODO ȭ���� ��ġ�ϼ���.";
                    break;
                case ETitleSceneState.LoginFail:
                    GetText((int)Texts.StatusText).text = "TODO �α��� ����";
                    break;
                case ETitleSceneState.ConnectingToGameServer:
                    GetText((int)Texts.StatusText).text = "TODO ���� ������";
                    break;
                case ETitleSceneState.ConnectedToGameServer:
                    GetText((int)Texts.StatusText).text = "TODO ���� ���� ����";
                    GetText((int)Texts.StartText).text = "TODO ȭ���� ��ġ�ϼ���.";
                    break;
                case ETitleSceneState.FailedToConnectToGameServer:
                    GetText((int)Texts.StatusText).text = "TODO ���� ���� ����";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        BindTexts(typeof(Texts));

        GetText(((int)Texts.StartText)).gameObject.BindEvent(OnClickStartButton);
        GetText(((int)Texts.StartText)).gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        // Load ����
        State = ETitleSceneState.AssetLoading;

        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, totalCount) =>
        {
            GetText((int)Texts.StatusText).text = $"TODO �ε��� : {key} {count}/{totalCount}";

            if (count == totalCount)
            {
                OnAssetLoaded();
            }
        });
    }

    private void OnAssetLoaded()
    {
        State = ETitleSceneState.AssetLoaded;

        ConnectToGameServer();

        // �ε� �Ϸ�Ǹ� �α���
        UI_LogInPopup popup = Managers.UI.ShowPopupUI<UI_LogInPopup>();
        popup.SetInfo(OnLoginSuccess);
    }

    private void OnClickStartButton(PointerEventData eventData)
    {

    }

    private void OnLoginSuccess(bool isSuccess)
    {
        if (isSuccess)
        {
            State = ETitleSceneState.LoginSuccess;
            GetText(((int)Texts.StartText)).gameObject.SetActive(true);
        }
        else
            State = ETitleSceneState.LoginFail;
    }

    private void ConnectToGameServer()
    {
        State = ETitleSceneState.ConnectingToGameServer;
        IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
        Managers.Network.GameServer.Connect(endPoint, OnGameServerConnectionSuccess, OnGameServerConnectionFailed);
    }

    private void OnGameServerConnectionSuccess()
    {
        State = ETitleSceneState.ConnectedToGameServer;
    }

    private void OnGameServerConnectionFailed()
    {
        State = ETitleSceneState.FailedToConnectToGameServer;
    }

    //public void OnAuthResHandler(S_AuthRes resPacket)
    //{
    //    if (State != ETitleSceneState.ConnectedToGameServer)
    //        return;

    //    if (resPacket.Success == false)
    //        return;

    //    // ���Ӽ����� ���� ��� ���ָ� ĳ���� ��� ��û.
    //    UI_SelectCharacterPopup popup = Managers.UI.ShowPopupUI<UI_SelectCharacterPopup>();
    //    popup.SendHeroListReqPacket();
    //}
}
