using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Rendering;

public class LobbyScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        SceneType = Define.EScene.LobbyScene;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;  
        GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
        GraphicsSettings.transparencySortAxis = new Vector3(0.0f, 1.0f, 0.0f);
        Application.runInBackground = true;

        // ���������� ���� ����Ѵ� (UI_TitleScene�� �ּ� �ε��� ����ϱ� ����)
        Managers.UI.SceneUI = GameObject.FindAnyObjectByType<UI_LobbyScene>();
    }

	protected override void Start()
    {
        base.Start();
	}

	public override void Clear()
	{

	}
}
