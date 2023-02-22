using Lindon.TowerUpper.Profile;
using UnityEngine;

namespace Lindon.UserManager.Page.Home
{
    public class StageInfo : MonoBehaviour
    {
        [Header("Slot")]
        [SerializeField] private Transform m_Parent;
        [SerializeField] private LevelSlot m_Prefab;
        [SerializeField] private LevelSlot m_BossPrefab;

        private void OnEnable()
        {
            ProfileController.Instance.OnLoadProfile += Load;
        }

        private void OnDisable()
        {
            ProfileController.Instance.OnLoadProfile -= Load;
        }

        public void Load()
        {
            Load(ProfileController.Instance.Profile);
        }

        private void Load(Profile profile)
        {
            var info = profile.GetChapter();
            m_Parent.DestroyChildren();

            for (int i = 0; i < 10; i++)
            {
                var bossLevel = i == 9;

                LevelSlot slot;
                if (!bossLevel)
                {
                    slot = Instantiate(m_Prefab, m_Parent);
                }
                else
                {
                    slot = Instantiate(m_BossPrefab, m_Parent);
                }

                var level = i + (10 * info.ChapterLevel);

                LevelSlot.Data data;
                if (i < info.GameLevel)
                {
                    data = new LevelSlot.Data(level, LevelSlot.Data.State.Passed, bossLevel);
                }
                else if (i == info.GameLevel)
                {
                    data = new LevelSlot.Data(level, LevelSlot.Data.State.Current, bossLevel);
                }
                else
                {
                    data = new LevelSlot.Data(level, LevelSlot.Data.State.Future, bossLevel);
                }

                slot.SetData(data);
            }
        }
    }
}