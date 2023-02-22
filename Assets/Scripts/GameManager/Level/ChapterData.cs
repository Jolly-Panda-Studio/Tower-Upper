using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    [CreateAssetMenu(fileName = "ChapterData", menuName = "Lindon/TowerUpper/Data/ChapterData")]
    public class ChapterData: ScriptableObject
    {
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
            return level switch
            {
                1 => m_Level1,
                2 => m_Level2,
                3 => m_Level3,
                4 => m_Level4,
                5 => m_Level5,
                6 => m_Level6,
                7 => m_Level7,
                8 => m_Level8,
                9 => m_Level9,
                10 => m_Level10,
                _ => null,
            };
        }

        public bool Equals(int Level) => m_Level == Level;
    }
}