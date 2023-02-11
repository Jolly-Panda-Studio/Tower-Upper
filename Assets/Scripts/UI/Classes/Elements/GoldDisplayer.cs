using Lindon.UserManager.Base.Element;
using TMPro;
using UnityEngine;

public class GoldDisplayer : UIElement
{
    [SerializeField] private TMP_Text m_ValueText;

    public override void DoCreate()
    {
    }

    protected override void SetValues()
    {
        m_ValueText.SetText($"{GoldCalculator.GoldAmount:n0}");
    }
}
