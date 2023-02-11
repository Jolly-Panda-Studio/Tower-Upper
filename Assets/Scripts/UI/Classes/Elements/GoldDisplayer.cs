using Lindon.TowerUpper.GameController;
using Lindon.UserManager.Base.Element;
using TMPro;
using UnityEngine;

public class GoldDisplayer : UIElement
{
    [SerializeField] private TMP_Text m_ValueText;

    public override void DoCreate()
    {
        GoldCalculator.GoldChanged += DisplayGoldAmount;
    }

    public override void OnDestory()
    {
        GoldCalculator.GoldChanged -= DisplayGoldAmount;
    }

    protected override void SetValues()
    {
        DisplayGoldAmount();
    }

    private void DisplayGoldAmount()
    {
        m_ValueText.SetText($"{GoldCalculator.GoldAmount:n0}");
    }
}
