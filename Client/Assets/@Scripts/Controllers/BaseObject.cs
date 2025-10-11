using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public int ObjectId { get; set; }
    public virtual EGameObjectType ObjectType { get { return EGameObjectType.None; } }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public void Move(Vector3 pos, bool isRotate)
    {

        transform.position += pos;

        if (isRotate)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 90);
        }
    }
}
