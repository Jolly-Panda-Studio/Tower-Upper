using UnityEngine;
using UnityEngine.UI;

namespace MJUtilities.UI
{
    public class SettingPopup : UIPopup
    {
        [SerializeField] private Button languageButton;
        [SerializeField] private Button webButton;

        public override void OnAwake()
        {
            languageButton.onClick.RemoveAllListeners();
            languageButton.onClick.AddListener(()=>UIManager.instance.OpenPopup(PopupType.Language));

            webButton.onClick.RemoveAllListeners();
            webButton.onClick.AddListener(() => Application.OpenURL("https://useffarahmand.com/"));
        }

        public override void OnSetValues()
        {
            BackButtonConfig();
        }
    }
}