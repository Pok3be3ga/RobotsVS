using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AxesEffect), menuName = "Effects/" + nameof(AxesEffect))]
public class AxesEffect : ContinuousEffect
{

    [SerializeField] private Axes _axesPrefab;
    public override void Activate()
    {
        base.Activate();
        AddSoundForEffect();
        if (Level == 0) {
            Axes axes = Instantiate(_axesPrefab);
            axes.Init(GetSkillValue(Skill.LifeTime), GetSkillValue(Skill.Damage), _player.transform);
        }
    }

    protected override void SetLevel()
    {
        base.SetLevel();
        Axes axes = Instantiate(_axesPrefab);
        axes.Init(GetSkillValue(Skill.LifeTime), GetSkillValue(Skill.Damage), _player.transform);

    }
}
