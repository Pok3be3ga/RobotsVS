using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

[CreateAssetMenu(fileName = nameof(FlamethowerEffect), menuName = "Effects/" + nameof(FlamethowerEffect))]
public class FlamethowerEffect : ContinuousEffect
{

    [SerializeField] private Flamethrower _flamethrowerPrefab;
    [SerializeField] private Flamethrower _flamethrower;

    private Collider[] _colliders = new Collider[20];
    [SerializeField] private LayerMask _layerMask;
    protected override void FirstTimeCreated()
    {
        base.FirstTimeCreated();
        _flamethrower = Instantiate(_flamethrowerPrefab, _player.transform.position, Quaternion.identity);
        _flamethrower.Init(_player.transform, GetSkillValue(Skill.Damage), GetSkillValue(Skill.Colldown));
    }

    protected override void SetLevel()
    {
        base.SetLevel();
        _flamethrower.Init(_player.transform, GetSkillValue(Skill.Damage), GetSkillValue(Skill.Colldown));

    }
}
