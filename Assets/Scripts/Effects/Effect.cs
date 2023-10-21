using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public string Name;
    [TextArea(1, 3)]
    public string Description;
    public Sprite Sprite;
    public int Level = -1;

    protected EffectsManager _effectsManager;
    protected Player _player;
    protected EnemyManager _enemyManager;



    public AudioSource _audioSource;
    public AudioManager _audioManager;

    public virtual void Initialize(EffectsManager effectsManager, EnemyManager enemyManager, Player player) 
    {
        _effectsManager = effectsManager;
        _player = player;
        _enemyManager = enemyManager;
    }

    public virtual void Activate() {
        Level++;
        if (Level == 1) {
            FirstTimeCreated();
        }
        SetLevel();
    }

    protected virtual void FirstTimeCreated()
    {
        _audioSource = _audioManager.FindAudioSourceByClipName(this.GetType().Name + "Sound01");
    }

    protected virtual void SetLevel() {
    }

}
