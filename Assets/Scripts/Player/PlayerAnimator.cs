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

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}