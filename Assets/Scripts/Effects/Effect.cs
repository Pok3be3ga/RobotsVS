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

    [HideInInspector] public AudioManager _audioManager;

    public virtual void Initialize(EffectsManager effectsManager, EnemyManager enemyManager, Player player)
    {
        _effectsManager = effectsManager;
        _player = player;
        _enemyManager = enemyManager;
    }

    public virtual void Initialize(EffectsManager effectsManager,
        EnemyManager enemyManager, Player player, AudioManager audioManager)
    {
        _effectsManager = effectsManager;
        _player = player;
        _enemyManager = enemyManager;
        _audioManager = audioManager;
    }

    public virtual void Activate()
    {
        Level++;
        if (Level == 1)
        {
            FirstTimeCreated();
        }
        SetLevel();
    }

    public virtual void FirstTimeCreated()
    {
        
    }

    protected virtual void SetLevel()
    {
    }

}
