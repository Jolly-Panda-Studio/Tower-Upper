using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.GameController;
using Lindon.TowerUpper.GameController.Events;
using Lindon.TowerUpper.Profile;
using Lindon.UserManager;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Initilizer
{
    public interface IInitilizer
    {
        void Init();
    }

    public static class GameInitilizer
    {
        private static List<IInitilizer> _initilizers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            FindController();

            foreach (var initilizer in _initilizers)
            {
                if (initilizer == null)
                {
                    Debug.LogError("A controller was not found");
                    continue;
                }
                initilizer.Init();
            }

            GameLoader.Load();
        }

        private static void FindController()
        {
            _initilizers = new List<IInitilizer>();

            var profileController = Object.FindObjectOfType<ProfileController>();
            _initilizers.Add(profileController);

            var gameData = Object.FindObjectOfType<GameData>();
            _initilizers.Add(gameData);

            var userInterfaceManager = Object.FindObjectOfType<UserInterfaceManager>();
            _initilizers.Add(userInterfaceManager);

            var gameManager = Object.FindObjectOfType<GameManager>();
            _initilizers.Add(gameManager);
        }
    }
}