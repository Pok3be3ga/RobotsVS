using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;


[CreateAssetMenu(fileName = nameof(RocketEffect), menuName = "Effects/" + nameof(RocketEffect))]
public class RocketEffect : ContinuousEffect
{

    [SerializeField] private Rocket _RocketPrefab;
    [SerializeField] private float _bulletSpeed;

    public float Period;

   
    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }
    IEnumerator Effectprocess()
    {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));
        Enemy[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, number);
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 position = _player.transform.position;
                Rocket magicMissles = Instantiate(_RocketPrefab, position, Quaternion.identity);
                magicMissles.Init(nearestEnemies[i], GetSkillValue(Skill.Damage), _bulletSpeed);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
