using Cinemachine;
using Lindon.TowerUpper.GameController;
using UnityEngine;

namespace Lindon.TowerUpper.Enviroment.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera m_Camera;

        private void Start()
        {
            m_Camera.LookAt = GameManager.Instance.Tower.Components.PlayerPosition;
        }
    }
}