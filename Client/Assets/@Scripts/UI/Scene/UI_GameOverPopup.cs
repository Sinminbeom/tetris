using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_GameOverPopup : UI_Popup
{
    enum Texts
    {
        ResultLabelText
    }

    enum Buttons
    {
        CloseButton
    }

    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickCloseButton);

        GetText((int)Texts.ResultLabelText).text = "Result";
    }

    public void OnGameOver(bool isWin)
    {
        GetText((int)Texts.ResultLabelText).text = isWin ? "You Win!" : "You Lose!";
    }

    public void OnClickCloseButton(PointerEventData evt)
    {
        ClosePopupUI();

        Managers.Scene.LoadScene(Define.EScene.LobbyScene);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        UI_RoomPopup roomPopup = Managers.UI.ShowPopupUI<UI_RoomPopup>();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
