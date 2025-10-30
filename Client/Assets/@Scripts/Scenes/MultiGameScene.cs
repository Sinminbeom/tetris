using UnityEngine;
using static Define;

public class MultiGameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("@>> MultiGameScene Init()");
        SceneType = EScene.MultiGameScene;

        Managers.Room.StartGame();
    }

	protected override void Start()
	{
		base.Start();
	}

	public override void Clear()
    {
    }
	
	void OnApplicationQuit()
	{
		Managers.Network.GameServer.Disconnect();
	}
}