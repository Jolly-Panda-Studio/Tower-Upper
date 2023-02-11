using Lindon.UserManager;
using Lindon.UserManager.Base.Element;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackButton : UIElement
{
    [SerializeField] private TMP_Text m_TitleText;
    [SerializeField] private Button m_Button;

    public override void DoCreate()
    {
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(() =>
        {
            UserInterfaceManager.OnBackPressed();
        });
    }

    protected override void SetValues()
    {
        var title = UserInterfaceManager.GetTitle();
        m_TitleText.SetText(title);
    }
}
