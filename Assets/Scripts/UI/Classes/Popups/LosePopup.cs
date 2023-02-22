using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : UIPopup
{
    [Header("Buttons")]
    [SerializeField] private Button m_HomeButton;
    [SerializeField] private Button m_ContinueButton;

    [Header("Quote")]
    [SerializeField] private TMP_Text m_QuoteText;
    [SerializeField,TextArea] private List<string> m_quotes;

    protected override void SetValues()
    {
        m_QuoteText.SetText(m_quotes.RandomItem());
    }

    protected override void SetValuesOnSceneLoad()
    {
        m_HomeButton.onClick.RemoveAllListeners();
        m_HomeButton.onClick.AddListener(Home);

        m_ContinueButton.onClick.RemoveAllListeners();
        m_ContinueButton.onClick.AddListener(Continue);
    }

    private void Home()
    {
        UserInterfaceManager.OnBackPressed();
        UserInterfaceManager.Open<HomePage>();
    }

    private void Continue()
    {
        Debug.Log("Not yet!!");
    }
}
