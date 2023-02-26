using Lindon.TowerUpper.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    public class LevelInfo : MonoBehaviour
    {
        [SerializeField] private Transform m_CharacterParent;
        [SerializeField] private Transform m_AimTarget;

        private List<PlayerModel> m_CurrentModels;

        private void Awake()
        {
            m_CurrentModels = new List<PlayerModel>();
        }

        private void Start()
        {
            m_CharacterParent = GameManager.Instance.Tower.Components.PlayerPosition;
        }

        public void ChangeItems(int skinId, int weaponId)
        {
            var model = GetFromCurrentModels(skinId);
            var playerModel = model == null ? InstantiateModel(skinId) : model;

            var player = playerModel.GetComponent<Player>();
            ActiveWeapon(weaponId, player);
        }

        private PlayerModel GetFromCurrentModels(int skinId)
        {
            PlayerModel playerModel = null;
            foreach (var model in m_CurrentModels)
            {
                model.gameObject.SetActive(false);
                if (model.Id == skinId)
                {
                    model.gameObject.SetActive(true);
                    playerModel = model;
                }
            }
            return playerModel;
        }

        private void ActiveWeapon(int weaponId, Player player)
        {
            var weaponObject = player.ActiveGun(weaponId);

            SetAimTarget(player);

            var weaponAim = weaponObject.GetAim();
            SetAimTransform(player, weaponAim);
        }

        private PlayerModel InstantiateModel(int skinId)
        {
            var skin = GameData.Instance.GetGameModel(skinId);
            var model = (PlayerModel)Instantiate(skin, m_CharacterParent);
            m_CurrentModels.Add(model);
            return model;
        }

        private void SetAimTarget(Player playerObject)
        {
            playerObject.SetAimTarget(m_AimTarget);
        }

        private void SetAimTransform(Player playerObject, Transform aimTransform)
        {
            playerObject.SetAimTransform(aimTransform);
        }
    }
}