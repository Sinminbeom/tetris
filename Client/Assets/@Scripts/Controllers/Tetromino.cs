using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : BaseObject
{
    private StateComponents stateComponents;

    protected override void Awake()
    {
        base.Awake();
        stateComponents = new StateComponents(this, new TetrominoStateLists(), (int)E_TETROMINO_STATE.Falling);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        UI_Popup popup = Managers.UI.GetPopupUI("UI_GamePopup");
        if (popup != null && popup.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Managers.UI.ClosePopupUI();
                Managers.Scene.LoadScene(Define.EScene.SingleGameScene);
            }
        }
        else
        {
            stateComponents.OnProcEveryFrame();
            stateComponents.OnChangeState();
        }
    }
}
