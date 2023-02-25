using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.EnemyUtility;
using Lindon.TowerUpper.EnemyUtility.Controller;
using Lindon.TowerUpper.GameController.Events;
using Lindon.TowerUpper.Initilizer;
using Lindon.TowerUpper.Profile;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

namespace Lindon.TowerUpper.GameController.Level
{
    public class LevelLoader : MonoBehaviour, IInitilizer
    {
        public static LevelLoader Instance { get; private set; }

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
            GameRestarter.OnRestartGame += StartGame;
            GameStarter.OnStartGame += StartGame;
            ProfileController.Instance.OnLoadProfile += LoadProfile;
            ProfileController.Instance.Profile.OnActiveItem += ChangeActiveItem;
        }

        private void OnDisable()
        {
            GameRestarter.OnRestartGame -= StartGame;
            GameStarter.OnStartGame -= StartGame;
            ProfileController.Instance.OnLoadProfile -= LoadProfile;
            ProfileController.Instance.Profile.OnActiveItem -= ChangeActiveItem;
        }

        private void LoadProfile(Profile.Profile profile)
        {
            var info = ProfileController.Instance.Profile.GetChapter();

            var gameInfo = GameData.Instance.GetGameInfo(info.ChapterLevel, info.GameLevel);

            Load(gameInfo, profile);
        }

        private void ChangeActiveItem(int id, ItemCategory category)
        {
            var profile = ProfileController.Instance.Profile;
            LoadCharacter(profile);
        }

        private void StartGame()
        {
            var profile = ProfileController.Instance.Profile;

            var info = ProfileController.Instance.Profile.GetChapter();

            var gameInfo = GameData.Instance.GetGameInfo(info.ChapterLevel, info.GameLevel);

            Load(gameInfo, profile);
        }

        private void Load(GameInfo gameInfo, Profile.Profile profile)
        {
            LoadCharacter(profile);
            LoadTower(gameInfo);
            LoadEnemyManager(gameInfo);
        }

        private void LoadCharacter(Profile.Profile profile)
        {
            var characterSkinId = profile.GetActiveItem(ItemCategory.Skin);
            var weaponId = profile.GetActiveItem(ItemCategory.Weapon);
            GameManager.Instance.LevelInfo.ChangeItems(characterSkinId, weaponId);
        }

        private void LoadTower(GameInfo gameInfo)
        {
            GameManager.Instance.Tower.Components.SpawnPointCreator.CreatePoints(gameInfo.SpawnPointCount);
        }

        private void LoadEnemyManager(GameInfo gameInfo)
        {
            EnemyCounter.TotalEnemy = gameInfo.MaxEnemyCount;

            foreach (var id in gameInfo.EnemiesId)
            {
                var prefab = GameData.Instance.GetEnemyModel(id);
                var enemyPrefab = prefab.GetOrAddComponent<Enemy>();
                GameManager.Instance.EnemyManager.Generator.AddPrefab(enemyPrefab);
            }

            if (gameInfo.HasBossFight)
            {
                var prefab = GameData.Instance.GetEnemyModel(gameInfo.BossEnemyId);
                var enemyPrefab = prefab.GetOrAddComponent<Enemy>();
                GameManager.Instance.EnemyManager.Generator.AddBossPrefab(enemyPrefab);
            }
        }
    }
}