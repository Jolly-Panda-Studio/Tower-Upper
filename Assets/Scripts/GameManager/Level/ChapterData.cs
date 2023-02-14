using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    [CreateAssetMenu(fileName = "ChapterData", menuName = "Lindon/TowerUpper/Data/ChapterData")]
    public class ChapterData: ScriptableObject
    {
        [SerializeField] private int m_Id;
        [SerializeField, Min(1)] private int m_Level;
        [SerializeField] private GameInfo m_Level1;
        [SerializeField] private GameInfo m_Level2;
        [SerializeField] private GameInfo m_Level3;
        [SerializeField] private GameInfo m_Level4;
        [SerializeField] private GameInfo m_Level5;
        [SerializeField] private GameInfo m_Level6;
        [SerializeField] private GameInfo m_Level7;
        [SerializeField] private GameInfo m_Level8;
        [SerializeField] private GameInfo m_Level9;
        [SerializeField] private GameInfo m_Level10;

        public GameInfo GetGame(int level)
        {
            level++;
            if (level == 1)
            {
                return m_Level1;
            }
            else if (level == 2)
            {
                return m_Level2;
            }
            else if(level == 3)
            {
                return m_Level3;
            }
            else if (level == 4)
            {
                return m_Level4;
            }
            else if (level == 5)
            {
                return m_Level5;
            }
            else if (level == 6)
            {
                return m_Level6;
            }
            else if (level == 7)
            {
                return m_Level7;
            }
            else if (level == 8)
            {
                return m_Level8;
            }
            else if (level == 9)
            {
                return m_Level9;
            }
            else if (level == 10)
            {
                return m_Level10;
            }
            return null;
        }

        public bool Equals(int Level) => m_Level == Level;
    }
}