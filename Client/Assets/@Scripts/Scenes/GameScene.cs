using UnityEngine;
using static Define;

public class GameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("@>> GameScene Init()");
        SceneType = EScene.GameScene;

        // Managers.Map.LoadMap("MMO_edu_map");
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