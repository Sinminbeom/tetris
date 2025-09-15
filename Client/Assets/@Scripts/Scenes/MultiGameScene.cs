using UnityEngine;
using static Define;

public class MultiGameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("@>> MultiGameScene Init()");
        SceneType = EScene.MultiGameScene;

        // �̹� �ٸ� ȭ�鿡�� GameRoom�� Id�� �̹� �����Ǿ�������
        // �̹� �ٸ� ȭ�鿡�� Player���� �� �����Ǿ���������
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