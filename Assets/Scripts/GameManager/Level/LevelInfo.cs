using Lindon.TowerUpper.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    public class LevelInfo : MonoBehaviour
    {
        [SerializeField] private Transform m_CharacterParent;
        [SerializeField] private Transform m_AimTarget;

        private ItemModel[] m_currentModels;

        private void Start()
        {
            m_currentModels = GetComponentsInChildren<ItemModel>();
        }

        public void SpawnCharacter(int skinId,int weaponId)
        {
            var checkModel = CheckCurrentModel(skinId);
            var playerObject = InstantiateModel(skinId);

            var checkWeapon = CheckCurrentModel(weaponId);

           //var weaponObject = InstantiateWeapon(weaponId);

            var weaponObject= playerObject.ActiveGun(weaponId);

            SetAimTarget(playerObject);

            var weaponAim = weaponObject.GetAim();
            SetAimTransform(playerObject, weaponAim);
        }

        private bool CheckCurrentModel(int newModelId)
        {
            if (m_currentModels.Length == 0) return true;

            foreach(var model in m_currentModels)
            {
                if(model.Equals(newModelId)) return false;
            }

            return true;
        }

        private void DestroyOldCharacter()
        {

        }

        private Player InstantiateModel(int skinId)
        {
            var skin = GameData.Instance.GetModel(skinId);
            var model = Instantiate(skin, m_CharacterParent);
            var playerObject = model.GetOrAddComponent<Player>();
            return playerObject;
        }

        private Weapon InstantiateWeapon(int weaponId)
        {
            var skin = GameData.Instance.GetModel(weaponId);
            var model = Instantiate(skin);
            var weaponObject = model.AddComponent<Weapon>();
            return weaponObject;
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