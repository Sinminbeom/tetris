using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTetromino : abMultiTetromino
{
    public StateComponents StateComponents { get; set; }
    protected override void Awake()
    {
        base.Awake();
        StateComponents = new StateComponents(this, new TetrominoStateLists(), (int)E_TETROMINO_STATE.Falling);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        StateComponents.OnChangeState();
        StateComponents.OnProcEveryFrame();
    }
}
