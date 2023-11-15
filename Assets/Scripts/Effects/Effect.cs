using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public abstract class Effect : ScriptableObject
{
    public string Name;
    [SerializeField] string NameEn;
    [SerializeField] string NameRu;
    [TextArea(1, 3)]
    public string Description;
    [SerializeField] string DescriptionEn;
    [SerializeField] string DescriptionRu;
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
        if (YandexGame.EnvironmentData.language == "en")
        {
            Name = NameEn;
            Description = DescriptionEn;
        }
        else if (YandexGame.EnvironmentData.language == "ru")
        {
            Name = NameRu;
            Description = DescriptionRu;
        }
        else
        {
            Name = NameRu;
            Description = DescriptionRu;
        }
    }

    public virtual void Initialize(EffectsManager effectsManager,
        EnemyManager enemyManager, Player player, AudioManager audioManager)
    {
        _effectsManager = effectsManager;
        _player = player;
        _enemyManager = enemyManager;
        _audioManager = audioManager;
        if (YandexGame.EnvironmentData.language == "en")
        {
            Name = NameEn;
            Description = DescriptionEn;
        }
        else if (YandexGame.EnvironmentData.language == "ru")
        {
            Name = NameRu;
            Description = DescriptionRu;
        }
        else
        {
            Name = NameRu;
            Description = DescriptionRu;
        }
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
