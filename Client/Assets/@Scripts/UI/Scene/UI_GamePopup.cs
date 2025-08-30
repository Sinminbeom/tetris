using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GamePopup : UI_Popup
{
    protected override void Awake()
    {
        base.Awake();

        UICanvas.renderMode = RenderMode.ScreenSpaceCamera;
        UICanvas.worldCamera = Camera.main;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
