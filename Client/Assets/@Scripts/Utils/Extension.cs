using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static void BindEvent(this GameObject go, Action<PointerEventData> action = null, Define.ETouchEvent type = Define.ETouchEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }

    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
    }

    public static void DestroyChildren(this GameObject go)
    {
        DestroyChildren(go.transform);
    }
}
