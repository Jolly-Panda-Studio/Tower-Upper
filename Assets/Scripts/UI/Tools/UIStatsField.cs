using TMPro;
using UnityEngine;

public class UIStatsField:MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private TMP_Text valueChangesText;

    [SerializeField] private Color increaseColor;
    [SerializeField] private Color decreaseColor;

    private float value;

    public void SetValue(float value)
    {
        valueText.SetText(value.ToString());
        this.value = value;

        valueChangesText.gameObject.SetActive(false);
    }

    public void SetValue(string value)
    {
        valueText.SetText(value);
        valueChangesText.gameObject.SetActive(false);
    }

    public void ChangeValue(float newValue)
    {
        if (newValue == 0) return;
        valueChangesText.gameObject.SetActive(true);
        var text = Mathf.Abs(newValue).ToString();

        valueChangesText.color = Color.white;

        if (value > newValue + value)
        {
            valueChangesText.color = decreaseColor;

            if (text[0] != '-')
            {
                text.Insert(0, "-");
            }
            //text = "- ";
        }
        else if (value < newValue + value)
        {
            valueChangesText.color = increaseColor;
            
                text.Insert(0, "+");
        }

        //text += newValue;

        valueChangesText.SetText(text);
    }
}
