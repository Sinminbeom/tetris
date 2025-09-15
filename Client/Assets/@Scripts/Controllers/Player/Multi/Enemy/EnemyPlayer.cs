using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnemyPlayer : abMultiPlayer
{
    public EnemyPlayer()
    {
    }

    public override void Init()
    {
        Board.Init();
        Board.Spawn();
    }
}