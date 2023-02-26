using Cinemachine;
using DG.Tweening;
using Lindon.TowerUpper.GameController.Events;
using UnityEngine;

namespace Lindon.TowerUpper.Enviroment.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera m_Camera;
        [SerializeField] private Transform m_InHomePosition;
        [SerializeField] private Transform m_InGamePosition;
        [SerializeField] private float m_Speed = 1;

        private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_mover;

        private void Start()
        {
            m_Camera.transform.position = m_InHomePosition.position;
        }

        private void OnEnable()
        {
            GameStarter.OnStartGame += GoGamePosition;
            ReturnHome.OnReturnHome += GoHomePosition;
            GameRunnig.OnChange += GameRunnig_OnChange;
        }

        private void OnDisable()
        {
            GameStarter.OnStartGame -= GoGamePosition;
            ReturnHome.OnReturnHome -= GoHomePosition;
            GameRunnig.OnChange -= GameRunnig_OnChange;
        }

        private void GameRunnig_OnChange(bool value)
        {
            if (m_mover == null) return;
            if (value)
            {
                m_mover.Play();
            }
            else
            {
                m_mover.Pause();
            }
        }

        private void GoGamePosition()
        {
            SwitchPosition(m_InGamePosition.position);
        }

        private void GoHomePosition()
        {
            SwitchPosition(m_InHomePosition.position);
        }

        private void SwitchPosition(Vector3 endValue)
        {
            m_mover = m_Camera.transform.DOMove(endValue, transform.GetDuration(endValue, m_Speed));
        }
    }
}