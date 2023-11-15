using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

public enum Skill
{
    Colldown,
    Damage,
    Radius,
    Number, //ProjectilesCount
    DPS,
    PassCount, //PassThroughCount
    LifeTime
}

[System.Serializable]
public class SkillLevels
{
    public float[] Values = new float[10];
}

public class ContinuousEffect : Effect
{

    [HideInInspector]
    public bool[] ActiveSkills = new bool[GetTotalNumberOfSkills()];
    [HideInInspector]
    public SkillLevels[] SkillLevels = new SkillLevels[GetTotalNumberOfSkills()];
    [HideInInspector]
    public SkillLevels[] SkillLevelsAdditions = new SkillLevels[GetTotalNumberOfSkills()];

    [HideInInspector]
    public UnityEvent<float> OnLoad = new UnityEvent<float>();
    private float _timer;
    protected int _clipNum = 1;

    [HideInInspector] public AudioSource _audioSource;

    // Строчки текста для перевода
    static string _colldownRu = "перезарядка";
    static string _colldownEn = "cooldown";
    static string _damageRu = "урон";
    static string _damageEn = "damage";
    static string _radiusRu = "радиус";
    static string _radiusEn = "radius";
    static string _numberRu = "количество";
    static string _numberEn = "number";
    static string _DPSRu = "урон в секунду";
    static string _DPSEn = "DPS";
    static string _passCountRu = "колличество целей";
    static string _passCountEn = "pass count";
    static string _lifeTimeRu = "время жизни";
    static string _lifeTimeEn = "life time";
    public static int GetTotalNumberOfSkills()
    {
        return Enum.GetValues(typeof(Skill)).Length;
    }

    public float GetSkillValue(Skill skill, bool nextLevel = false)
    {
        int levelIndex = Level - 1 + (nextLevel ? 1 : 0);

        float value = SkillLevels[(int)skill].Values[levelIndex];

        if (skill == Skill.Colldown)
        {
            value *= (1 - _player.ColldownReduction);
        }
        else if (skill == Skill.Damage)
        {
            value *= (1 + _player.DamageBoost + (YandexGame.savesData.DamageLevel) * 0.5f);
        }
        else if (skill == Skill.Radius)
        {
            value *= (1 + _player.RadiusBoost);
        }
        else if (skill == Skill.Number)
        {
            value += _player.ProjectileCount;
        }
        else if (skill == Skill.DPS)
        {
            value += _player.DamageBoost;
        }
        return value;
    }

    public void ProcessFrame(float frameTime)
    {
        _timer += frameTime;
        float colldown = GetSkillValue(Skill.Colldown);

        OnLoad.Invoke(_timer / colldown);

        if (_timer > colldown)
        {
            _timer = 0;
            Produce();
        }
    }

    protected virtual void Produce()
    {
        if (_audioSource != null /*_audioManager.CheckAvailability(this.GetType().Name + "Sound0" + _clipNum)*/)
        {
            _audioSource.Play();
        }

        if (!_audioManager.CheckAvailability(this.GetType().Name + "Sound0" + (_clipNum + 1)))
        {
            _clipNum = 1;
        }
        else
        {
            _clipNum += 1;
        }
    }

    public static string GetSkillName(Skill skill)
    {
        if (skill == Skill.Colldown && YandexGame.EnvironmentData.language == "en")
            return _colldownEn;
        else if (skill == Skill.Colldown && YandexGame.EnvironmentData.language == "ru")
            return _colldownRu;

        else if (skill == Skill.Damage && YandexGame.EnvironmentData.language == "en")
            return _damageEn;
        else if (skill == Skill.Damage && YandexGame.EnvironmentData.language == "ru")
            return _damageRu;

        else if (skill == Skill.Radius && YandexGame.EnvironmentData.language == "ru")
            return _radiusRu;
        else if (skill == Skill.Radius && YandexGame.EnvironmentData.language == "en")
            return _radiusEn;

        else if (skill == Skill.Number && YandexGame.EnvironmentData.language == "en")
            return _numberEn;
        else if (skill == Skill.Number && YandexGame.EnvironmentData.language == "ru")
            return _numberRu;

        else if (skill == Skill.DPS && YandexGame.EnvironmentData.language == "en")
            return _DPSEn;
        else if (skill == Skill.DPS && YandexGame.EnvironmentData.language == "ru")
            return _DPSRu;

        else if (skill == Skill.PassCount && YandexGame.EnvironmentData.language == "en")
            return _passCountEn;
        else if (skill == Skill.PassCount && YandexGame.EnvironmentData.language == "ru")
            return _passCountRu;

        else if (skill == Skill.LifeTime && YandexGame.EnvironmentData.language == "en")
            return _lifeTimeEn;
        else if (skill == Skill.LifeTime && YandexGame.EnvironmentData.language == "ru")
            return _lifeTimeRu;
        else return "Нет названия для такого эффекта";
    }

    public void AddSoundForEffect()
    {
        if (_audioManager.CheckAvailability(this.GetType().Name + "Sound01"))
            _audioSource = _audioManager.FindAudioSourceByClipName(this.GetType().Name + "Sound01");
    }
}
