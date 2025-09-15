using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SingleTetromino : Tetromino
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