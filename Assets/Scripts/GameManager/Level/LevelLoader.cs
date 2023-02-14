using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.Initilizer;
using Lindon.TowerUpper.Profile;
using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    public class LevelLoader : MonoBehaviour, IInitilizer
    {
        public static LevelLoader Instance { get; private set; }

        [Header("Component")]
        [SerializeField] private LevelInfo m_LevelInfo;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            GameStarter.OnStartGame += StartGame;
        }

        private void OnDisable()
        {
            GameStarter.OnStartGame += StartGame;
        }

        private void StartGame()
        {
            var profile = ProfileController.Instance.Profile;

            var info = GetChapter(profile.Level);

            var gameInfo = GameData.Instance.GetGameInfo(info.Item1, info.Item2);

            Load(gameInfo, profile);
        }

        private (int, int) GetChapter(int profileLevel)
        {
            var chapterLevel = (profileLevel % 10);
            var gameLevel = profileLevel - chapterLevel * 10;
            return (chapterLevel + 1, gameLevel);
        }

        private void Load(GameInfo levelData, Profile.Profile profile)
        {
            LoadCharacter(profile);

        }

        private void LoadCharacter(Profile.Profile profile)
        {
            var characterSkinId = profile.GetActiveItem(ItemCategory.Skin);
            var weaponId = profile.GetActiveItem(ItemCategory.Weapon);
            m_LevelInfo.SpawnCharacter(characterSkinId, weaponId);
        }
    }
}