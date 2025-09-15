using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class abBackground : IBackground
{
    public IBoard Board { get; set; }

    public GameObject Root { get; set; } = new GameObject("@Background");

    public abstract void Init();
}
