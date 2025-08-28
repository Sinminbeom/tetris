using UnityEngine;
using static Define;

public class SingleGameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("@>> SingleGameScene Init()");
        SceneType = EScene.SingleGameScene;

        Managers.Board.LoadBoard();
        Managers.Background.LoadBackground();
        Managers.Object.LoadTetromino();
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