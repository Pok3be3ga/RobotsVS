using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MineEffect), menuName = "Effects/" + nameof(MineEffect))]
public class MineEffect : ContinuousEffect
{

    [SerializeField] private Mine _minePrefab;
    private AudioSource[] _mineExplosionSound;

    private void Awake()
    {
        
    }

    protected override void Produce()
    {

        base.Produce();
        Mine newMine = Instantiate(_minePrefab, _player.transform.position, Quaternion.identity);
        newMine.Init(GetSkillValue(Skill.Damage), GetSkillValue(Skill.Radius), 
            _mineExplosionSound[Random.Range(0, _mineExplosionSound.Length)]);
    }

}
