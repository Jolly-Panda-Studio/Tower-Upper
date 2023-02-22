using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lindon.UserManager.Page.Home
{
    public class LevelSlot : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TMP_Text m_LevelText;

        [Header("Background")]
        [SerializeField] private Image m_Background;
        [SerializeField] private Sprite m_PassedLevelSprite;
        [SerializeField] private Sprite m_CurrentLevelSprite;
        [SerializeField] private Sprite m_FutureLevelSprite;

        [Header("Other")]
        [SerializeField] private Image m_BossIcon;
        [SerializeField] private Animator m_Aniamtor;

        public struct Data
        {
            public Data(int level, State currentState, bool isBossLevel)
            {
                Level = ++level;
                CurrentState = currentState;
                IsBossLevel = isBossLevel;
            }

            public int Level { get; private set; }
            public State CurrentState { get; private set; }
            public bool IsBossLevel { get; private set; }

            public enum State
            {
                Passed,
                Current,
                Future
            }
        }

        public void SetData(Data data)
        {
            SetLevelText(data);
            SetBackground(data);
            PlayAnimation(data);
        }

        private void SetLevelText(Data data)
        {
            if (data.IsBossLevel)
            {
                m_LevelText.gameObject.SetActive(false);
                m_BossIcon?.gameObject.SetActive(true);
            }
            else
            {
                m_LevelText.gameObject.SetActive(true);
                m_BossIcon?.gameObject.SetActive(false);
                m_LevelText.SetText(data.Level.ToString());
            }
        }

        private void SetBackground(Data data)
        {
            Sprite backgroundSprite = null;
            switch (data.CurrentState)
            {
                case Data.State.Passed:
                    backgroundSprite = m_PassedLevelSprite;
                    break;
                case Data.State.Current:
                    backgroundSprite = m_CurrentLevelSprite;
                    break;
                case Data.State.Future:
                    backgroundSprite = m_FutureLevelSprite;
                    break;
            }
            m_Background.sprite = backgroundSprite;
        }

        private void PlayAnimation(Data data)
        {
            m_Aniamtor.enabled = data.CurrentState == Data.State.Current;
        }
    }
}