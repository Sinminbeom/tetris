using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : abState
{
    public IdleState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        Managers.UI.ShowPopupUI<UI_GamePopup>();
    }

    public override void OnLeave()
    {
        Managers.UI.ClosePopupUI();
        //Managers.Scene.LoadScene(Define.EScene.SingleGameScene);
        Managers.Scene.LoadScene(Define.EScene.MultiGameScene);
    }

    public override void OnProcEveryFrame()
    {
        UI_Popup popup = Managers.UI.GetPopupUI("UI_GamePopup");
        if (popup != null && popup.gameObject.activeSelf)
        {
            if (IsClicked())
            {
                StateComponents stateComponents = GetStateComponents();
                stateComponents.ChangeState((int)E_TETROMINO_STATE.Falling);
            }
        }
    }

    public override void OnProcOnce()
    {
    }

    private bool IsClicked()
    {
        // 모바일 클릭체크
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return true;

        // PC esc 누름체크
        if (Input.GetKeyDown(KeyCode.Escape))
            return true;

        return false;
    }
}
