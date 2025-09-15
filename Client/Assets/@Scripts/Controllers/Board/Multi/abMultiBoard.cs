using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class abMultiBoard : abBoard
{
    protected float camHeight;
    protected float camWidth;

    public abMultiBoard()
    {
        this.camHeight = Camera.main.orthographicSize * 2f;
        this.camWidth = camHeight * Camera.main.aspect;
    }
}