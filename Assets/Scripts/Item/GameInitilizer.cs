using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.Profile;
using Lindon.UserManager;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Lindon.TowerUpper.Initilizer
{
    public interface IInitilizer
    {
        void Init();
    }

    public static class GameInitilizer
    {
        private static ProfileController m_ProfileController;
        private static UserInterfaceManager m_UserInterfaceManager;
        private static GameData m_GameData;
        private static List<IInitilizer> _initilizers;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            FindController();

            foreach (var initilizer in _initilizers)
            {
                initilizer.Init();
            }
        }

        private static void FindController()
        {
            _initilizers = new List<IInitilizer>();

            m_ProfileController = Object.FindObjectOfType<ProfileController>();
            _initilizers.Add(m_ProfileController);
            m_GameData = Object.FindObjectOfType<GameData>();
            _initilizers.Add(m_GameData);
            m_UserInterfaceManager = Object.FindObjectOfType<UserInterfaceManager>();
            _initilizers.Add(m_UserInterfaceManager);
        }
    }
}