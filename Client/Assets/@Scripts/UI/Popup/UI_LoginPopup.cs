using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LoginPopup : UI_Popup
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
        // 1) TODO : 인증서버로 인증 요청
        // 2) TODO : 인증서버에서 인증 성공하면, AccountDbId 및 JWT 받아와서 이어서 진행.

        C_LogInReq logInReq = new C_LogInReq();
        logInReq.Email = GetEmail();
        logInReq.Password = GetPassword();

        Managers.Network.Send(logInReq);

        // TODO 확인
        //_onClosePopup?.Invoke(true);
        //ClosePopupUI();
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
        ClosePopupUI();
    }

    void OnClickSignUpButton(PointerEventData evt)
    {
        UI_SignUpPopup signUpPopup = Managers.UI.ShowPopupUI<UI_SignUpPopup>();
    }
}
