using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SignUpPopup : UI_Popup
{

    enum Buttons
    {
        SignUpButton,
    }

    enum Texts
    {
        NameText,
        EmailText,
        PasswordText
    }
    
    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.SignUpButton).gameObject.BindEvent(OnClickSignUpButton);
    }

    public void SetInfo(Action<int> action)
    {

    }

    void OnClickSignUpButton(PointerEventData evt)
    {
        C_SignUpReq signUpReq = new C_SignUpReq();
        signUpReq.Name = GetName();
        signUpReq.Email = GetEmail();
        signUpReq.Password = GetPassword();

        Debug.Log(signUpReq);

        Managers.Network.Send(signUpReq);
    }

    public void OnSignUpHandler(S_SignUpRes signUpRes)
    {
        if (signUpRes.Result == ESignUpResult.Success)
        {
            ClosePopupUI();
        } else
        {
            // TODO: 회원가입 실패
        }
    } 
    string GetName()
    {
        return GetText((int)Texts.NameText).text;
    }

    string GetEmail()
    {
        return GetText((int)Texts.EmailText).text;
    }

    string GetPassword()
    {
        return GetText((int)Texts.PasswordText).text;
    }
}
