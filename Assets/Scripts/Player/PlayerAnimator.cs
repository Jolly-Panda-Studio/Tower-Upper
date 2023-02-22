using Lindon.TowerUpper.AnimatorController;
using Lindon.TowerUpper.GameController.Events;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : AnimatorController
{
    [Header("Parameters")]
    [SerializeField] private string m_FireParameter;

    private void OnEnable()
    {
        GameStarter.OnStartGame += StartGame;
        GameFinisher.OnFinishGame += FinishGame;
    }

    private void OnDisable()
    {
        GameStarter.OnStartGame -= StartGame;
        GameFinisher.OnFinishGame -= FinishGame;
    }

    protected override void FinishGame()
    {
        PlayGunFire(false);
        base.FinishGame();
    }

    protected override void StartGame()
    {
        PlayGunFire(true);
        base.StartGame();
    }


    public void PlayGunFire(bool state)
    {
        m_animator.SetBool(m_FireParameter, state);
    }
}
