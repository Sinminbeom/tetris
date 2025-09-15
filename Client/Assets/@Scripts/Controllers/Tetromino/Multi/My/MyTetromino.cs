using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTetromino : abMultiTetromino
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

        stateComponents.OnChangeState();
        stateComponents.OnProcEveryFrame();
    }
}
