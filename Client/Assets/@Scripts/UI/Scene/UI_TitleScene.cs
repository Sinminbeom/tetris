using System.Net;
using UnityEngine;
using static Define;

public class UI_TitleScene : UI_Scene
{
    private enum GameObjects
    {
        StartButton,
    }

    private enum Texts
    {
        StatusText,
    }

    private enum TitleSceneState
    {
        None,
        AssetLoading,
        AssetLoaded,
        ConnectingToServer,
        ConnectedToServer,
        FailedToConnectToServer,
    }

    TitleSceneState _state = TitleSceneState.None;

    TitleSceneState State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (value)
            {
                case TitleSceneState.None:
                    break;
                case TitleSceneState.AssetLoading:
                    GetText((int)Texts.StatusText).text = $"TODO �ε���";
                    break;
                case TitleSceneState.AssetLoaded:
                    GetText((int)Texts.StatusText).text = "TODO �ε� �Ϸ�";
                    break;
                case TitleSceneState.ConnectingToServer:
                    GetText((int)Texts.StatusText).text = "TODO ���� ������";
                    break;
                case TitleSceneState.ConnectedToServer:
                    GetText((int)Texts.StatusText).text = "TODO ���� ���� ����";
                    break;
                case TitleSceneState.FailedToConnectToServer:
                    GetText((int)Texts.StatusText).text = "TODO ���� ���� ����";
                    break;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(GameObjects));
        BindTexts(typeof(Texts));

        GetObject((int)GameObjects.StartButton).BindEvent((evt) =>
        {
            //Managers.Scene.LoadScene(EScene.SingleGameScene);
            Managers.Scene.LoadScene(EScene.MultiGameScene);
        });

        GetObject((int)GameObjects.StartButton).gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        // Load ����
        State = TitleSceneState.AssetLoading;

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
        State = TitleSceneState.AssetLoaded;
        //Managers.Data.Init();

        Debug.Log("Connecting To Server");
        State = TitleSceneState.ConnectingToServer;

        // TODO �ӽ�
        GetObject((int)GameObjects.StartButton).gameObject.SetActive(true);

        //IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
        //IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
        //Managers.Network.GameServer.Connect(endPoint, OnConnectionSuccess, OnConnectionFailed);
    }

    private void OnConnectionSuccess()
    {
        Debug.Log("Connected To Server");
        State = TitleSceneState.ConnectedToServer;

        GetObject((int)GameObjects.StartButton).gameObject.SetActive(true);
    }

    private void OnConnectionFailed()
    {
        Debug.Log("Failed To Connect To Server");
        State = TitleSceneState.FailedToConnectToServer;
    }
}
