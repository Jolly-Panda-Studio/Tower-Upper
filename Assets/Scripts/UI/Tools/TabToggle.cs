using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TabToggle : MonoBehaviour
{
    [SerializeField] private TabGroup tabGroup;

    Toggle myToggle;
    [SerializeField] private IconMode iconMode = IconMode.Image;
    [SerializeField ] private Image iconImage;
    [SerializeField] private TMP_Text iconText;
    //[SerializeField] private GameObject tabFocus;
    [SerializeField] private Color focusColor;
    [SerializeField] private Color normalColor;

    [Header("Events")]
    [SerializeField] private UnityEvent onFocus;
    [SerializeField] private UnityEvent onRemovefocus;

    public void SetData(ToggleGroup group, UnityAction<bool> onValueChangedCallback)
    {
        myToggle = GetComponent<Toggle>();
        myToggle.group = group;

        myToggle.onValueChanged.RemoveAllListeners();
        myToggle.onValueChanged.AddListener((value) =>
        {
            onValueChangedCallback?.Invoke(value);

            if (value)
            {
                OnFocus();
            }
            else
            {
                RemoveFocus();
            }
        });

        RemoveFocus();
    }

    private void OnFocus()
    {
        if (iconMode == IconMode.Image)
        {
            iconImage.color = focusColor;
        }
        else
        {
            iconText.color = focusColor;
        }
        onFocus?.Invoke();
    }

    private void RemoveFocus()
    {
        if (iconMode == IconMode.Image)
        {
            iconImage.color = normalColor;
        }
        else
        {
            iconText.color = normalColor;
        }
        onRemovefocus?.Invoke();
    }

    public void SetOn(bool value)
    {
        myToggle.isOn = value;
        //myToggle.onValueChanged?.Invoke(value);
    }

    enum IconMode
    {
        Image,
        Text
    }
}
