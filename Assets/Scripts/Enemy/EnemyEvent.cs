using Lindon.TowerUpper.GameController.Events;

namespace Lindon.TowerUpper.EnemyUtility.Component
{
    public class EnemyEvent : BaseComponent
    {
        protected override void OnCreate()
        {

        }

        public override void RegisterEvents()
        {
            GameRunnig.OnChange += OnChangeRunnig;
            GameFinisher.OnFinishGame += GameFinished;
            GameRestarter.OnRestartGame += GameFinished;
            ReturnHome.OnReturnHome += OnReturnHome;
        }

        public override void UnregisterEvents()
        {
            GameRunnig.OnChange -= OnChangeRunnig;
            GameFinisher.OnFinishGame -= GameFinished;
            GameRestarter.OnRestartGame -= GameFinished;
            ReturnHome.OnReturnHome -= OnReturnHome;
        }

        private void OnChangeRunnig(bool state)
        {
            m_Enemy.Climbing.ChangeClimbingRunnig(state);
        }

        private void GameFinished()
        {
            m_Enemy.Climbing.StopClimbing();
        }

        private void OnReturnHome()
        {
            Destroy(m_Enemy.gameObject);
        }
    }
}