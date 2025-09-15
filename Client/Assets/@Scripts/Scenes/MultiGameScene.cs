using UnityEngine;
using static Define;

public class MultiGameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("@>> MultiGameScene Init()");
        SceneType = EScene.MultiGameScene;

        // 이미 다른 화면에서 GameRoom의 Id가 이미 설정되엇을거임
        // 이미 다른 화면에서 Player까지 다 설정되어잇을거임
        Managers.GameRoom.Init();
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
		//Managers.Network.GameServer.Disconnect();
	}
}