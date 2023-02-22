using Lindon.TowerUpper.GameController.Events;
using Lindon.TowerUpper.Manager.Enemies;
using Lindon.TowerUpper.Profile;
using Lindon.UserManager;
using System;
using UnityEngine;

namespace Lindon.TowerUpper.GameController
{
    public class GameStateController : MonoBehaviour
    {
        private void OnEnable()
        {
            EnemyCounter.OnKillEnemy += KillEnemy;
            EnemyCounter.OnEnemyReached += EnemyReached;
            GameStarter.OnStartGame += StartGame;
            GameResault.OnLose += OnLose;
            GameResault.OnWin += OnWin;
        }

        private void OnDisable()
        {
            EnemyCounter.OnKillEnemy -= KillEnemy;
            EnemyCounter.OnEnemyReached -= EnemyReached;
            GameStarter.OnStartGame -= StartGame;
            GameResault.OnLose -= OnLose;
            GameResault.OnWin -= OnWin;
        }

        private void KillEnemy(int killedCount,int totalCount)
        {
            CheckWin(killedCount, totalCount);
        }

        private void EnemyReached(int rechedCount)
        {
            CheckLose(rechedCount);
        }

        private void StartGame()
        {
            GameRunnig.IsRunning = true;
        }

        private void CheckWin(int killedCount, int totalCount)
        {
            if (killedCount == totalCount)
            {
                OnWin();
                GameRunnig.IsRunning = false;
                GameFinisher.FinishGame();
            }
        }

        private void CheckLose(int rechedCount)
        {
            if (rechedCount > 0)
            {
                OnLose();
                GameRunnig.IsRunning = false;
                GameFinisher.FinishGame();
            }
        }

        private void OnLose()
        {
            Debug.Log("Lose Game");
            UserInterfaceManager.Open<HomePage>();
        }

        private void OnWin()
        {
            Debug.Log("Win Game");
            ProfileController.Instance.Profile.WinGame();
            UserInterfaceManager.Open<HomePage>();
        }
    }
}