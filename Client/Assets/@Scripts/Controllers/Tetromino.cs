using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : BaseObject
{

    protected override void Start()
    {
        base.Awake();
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
            Vector3 moveDir = Vector3.zero;
            bool isRotate = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDir.x = -1;

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDir.x = 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isRotate = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDir.y = -1;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                while (Managers.Board.MoveTo(this, Vector3.down, false))
                {
                }
            }

            if (Time.time > Managers.Board.nextFallTime)
            {
                Managers.Board.nextFallTime = Time.time + Managers.Board.fallCycle;
                moveDir = Vector3Int.down;
                isRotate = false;
            }

            if (moveDir != Vector3.zero || isRotate)
            {
                Managers.Board.MoveTo(this, moveDir, isRotate);
            }
        }
    }
}
