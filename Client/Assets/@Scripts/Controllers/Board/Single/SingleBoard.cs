using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SingleBoard : abBoard
{
    public SingleBoard()
    {
        Root = Utils.CreateObject("@Board");
    }

    public override void Init()
    {
        
    }
}