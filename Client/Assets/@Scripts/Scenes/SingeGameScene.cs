using UnityEngine;
using static Define;

public class SingleGameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("@>> SingleGameScene Init()");
        SceneType = EScene.SingleGameScene;

        Managers.SingleBoard.LoadBoard();
        Managers.SingleBackground.LoadBackground();
        Managers.SingleObject.LoadTetromino();
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