using UnityEngine;
using UnityEngine.UI;

namespace MJUtilities.UI
{
    public class ConfirmPopup : UIPopup
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        public bool loadScene;

        public override void OnAwake()
        {
            confirmButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
            
            confirmButton.onClick.AddListener(OnConfirmClicked);
            cancelButton.onClick.AddListener(OnCancelClicked);
        }
        public override void OnSetValues()
        {
            
        }

        private void OnConfirmClicked()
        {
            if(loadScene){
                // GameManager.instance.LoadScene();
                }
            //else
                //GameManager.instance.LoadScenario();
        } 
        
        private void OnCancelClicked()
        {
            CloseThisPopup();
        }

    }
}