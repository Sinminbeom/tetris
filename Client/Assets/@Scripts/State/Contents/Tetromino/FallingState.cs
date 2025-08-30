using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingState : abState
{
    private Vector2 touchStartPos;
    private float dragThreshold = 50f; // �巡�� �ּ� �Ÿ�

    public FallingState(StateLists stateLists, int stateId)
        : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {

    }

    public override void OnLeave()
    {
        
    }

    public override void OnProcEveryFrame()
    {
        StateComponents stateComponents = GetStateComponents();
        Tetromino tetromino = (Tetromino)stateComponents.GetParentProcess();

        // �����
        HandleTouchInput(stateComponents);
        // PC
        HandleKeyboardInput(stateComponents);

        if (Time.time > Managers.Board.nextFallTime)
        {
            Managers.Board.nextFallTime = Time.time + Managers.Board.fallCycle;
            if (!Managers.Board.MoveTo(tetromino, Vector3Int.down, false))
            {
                stateComponents.ChangeState((int)E_TETROMINO_STATE.Locked);
            }
        }
    }

    public override void OnProcOnce()
    {
        
    }

    private void HandleTouchInput(StateComponents stateComponents)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 delta = touch.position - touchStartPos;

                if (delta.magnitude < dragThreshold)
                {
                    // Ŭ�� �� ȸ��
                    stateComponents.ChangeState((int)E_TETROMINO_STATE.Rotating);
                }
                else
                {
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        if (delta.x > 0)
                            stateComponents.ChangeState((int)E_TETROMINO_STATE.RightMoving); // ������ �̵�
                        else
                            stateComponents.ChangeState((int)E_TETROMINO_STATE.LeftMoving);// ���� �̵�
                    }
                    else
                    {
                        if (delta.y < 0)
                        {
                            // �Ʒ��� �巡�� �� ���
                            stateComponents.ChangeState((int)E_TETROMINO_STATE.Dropping);
                        }
                    }
                }
            }
        }
    }

    private void HandleKeyboardInput(StateComponents stateComponents)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.LeftMoving);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.DownMoving);

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.RightMoving);

        else if (Input.GetKeyDown(KeyCode.UpArrow))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Rotating);

        else if (Input.GetKeyDown(KeyCode.Space))
            stateComponents.ChangeState((int)E_TETROMINO_STATE.Dropping);

    }
}
