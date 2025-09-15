using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IPlayer
{
    void Init();
    string Name { get; set; }
    public IBoard Board { get; set; }
}
