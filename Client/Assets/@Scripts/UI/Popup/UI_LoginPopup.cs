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
        // 1) TODO : ���������� ���� ��û
        // 2) TODO : ������������ ���� �����ϸ�, AccountDbId �� JWT �޾ƿͼ� �̾ ����.

        C_LogInReq logInReq = new C_LogInReq();
        logInReq.Email = GetEmail();
        logInReq.Password = GetPassword();

        Managers.Network.Send(logInReq);

        // TODO Ȯ��
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
