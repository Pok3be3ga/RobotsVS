using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphereEffect : ContinuousEffect
{
    [SerializeField] GameObject Prefab;
    public override void FirstTimeCreated()
    {
        base.FirstTimeCreated();
        //Sphere = Instantiate(Prefab, _player.transform);
        //_currentNova.transform.localPosition = Vector3.zero;
    }

    protected override void SetLevel()
    {
        base.SetLevel();
        //_currentNova.SetRadius(GetSkillValue(Skill.Radius));
    }
}
