using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LogInPopup : UI_Popup
{
    enum GameObjects
    {
        CloseArea
    }

    enum Buttons
    {
        LoginButton,
        SignUpButton,
    }

    enum Texts
    {
        EmailText,
        PasswordText,
    }

    Action<bool> _onClosePopup;

    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));
        BindObjects(typeof(GameObjects));

        GetObject((int)GameObjects.CloseArea).BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnClickLoggInButton);
        GetButton((int)Buttons.SignUpButton).gameObject.BindEvent(OnClickSignUpButton);
    }

    public void SetInfo(Action<bool> action)
    {
        _onClosePopup = action;
    }

    void OnClickLoggInButton(PointerEventData evt)
    {
        // 1) TODO : ���������� ���� ��û
        // 2) TODO : ������������ ���� �����ϸ�, AccountDbId �� JWT �޾ƿͼ� �̾ ����.

        C_LogInReq logInReq = new C_LogInReq();
        logInReq.Email = GetEmail();
        logInReq.Password = GetPassword();

        Managers.Network.Send(logInReq);
    }

    public void OnLogInResHandler(S_LogInRes logInRes)
    {
        if (logInRes.Result == ELogInResult.Success)
        {
            _onClosePopup?.Invoke(true);
            ClosePopupUI();

            Managers.Player.MyPlayerInfo = logInRes.PlayerInfo;

            Managers.Scene.LoadScene(Define.EScene.LobbyScene);
        }
        else
        {
            // TODO : UI ǥ��
            // logInRes.Result
        }
    }

    string GetEmail()
    {
        return GetText((int)Texts.EmailText).text;
    }

    string GetPassword()
    {
        return GetText((int)Texts.PasswordText).text;
    }

    void OnClickCloseButton(PointerEventData evt)
    {
        //ClosePopupUI();
    }

    void OnClickSignUpButton(PointerEventData evt)
    {
        UI_SignUpPopup signUpPopup = Managers.UI.ShowPopupUI<UI_SignUpPopup>();
    }
}
