using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJUtilities.UI
{
    public class CountdownPopup : UIPopup
    {
        [Header("Buttons")]
        [SerializeField] private Button quitButton;
        [SerializeField] private Button startWaveButton;
        
        [Header("CountDown")]
        [SerializeField] private TMP_Text countDownText;
        [SerializeField] private float startTime = 10f;
        private float currentTime;
        private bool isCountingDown;
        
        
        public override void OnAwake()
        {
            
        }

        public override void OnSetValues()
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(QuitButtonClicked); 
            
            startWaveButton.onClick.RemoveAllListeners();
            startWaveButton.onClick.AddListener(StartWaveButtonClicked);

            StartCountdown();
        }
        
        private void QuitButtonClicked()
        {
            GameManager.instance.RestartGame();
        }

        private void StartWaveButtonClicked()
        {
            UIManager.instance.ClosePopup(PopupType.Countdown);
            StartNextRound();
        }
        
        
        public void StartCountdown()
        {
            currentTime = startTime;
            StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            isCountingDown = true;

            while (currentTime > 0)
            {
                countDownText.text = Mathf.CeilToInt(currentTime).ToString() + "s";
                yield return new WaitForSecondsRealtime(1f);
                currentTime -= 1f;
            }

            countDownText.text = "0";
            isCountingDown = false;

            StartNextRound();
        }

        private void StartNextRound()
        {
            Debug.Log("Next round started!");
            CloseThisPopup();
            //ToDo: start wave
        }
    }
}