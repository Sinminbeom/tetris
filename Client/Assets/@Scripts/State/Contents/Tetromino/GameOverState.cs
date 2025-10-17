using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameOverState : abState
{
    public GameOverState(StateLists stateLists, int stateId)
    : base(stateLists, stateId)
    {
    }

    public override void OnEnter()
    {
        UI_GameOverPopup gameOverPopup = Managers.UI.ShowPopupUI<UI_GameOverPopup>();
        gameOverPopup.OnGameOver(false);

        C_GameOver gameOver = new C_GameOver();
        Managers.Network.Send(gameOver);
    }

    public override void OnLeave()
    {
        
    }

    public override void OnProcEveryFrame()
    {
        
    }

    public override void OnProcOnce()
    {
        
    }
}
