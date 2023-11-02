using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ChainLightningEffect), menuName = "Effects/" + nameof(ChainLightningEffect))]
public class ChainLightningEffect : ContinuousEffect
{

    [SerializeField] private ChainLightning _chainLightningPrefab;
    [SerializeField] private float _bulletSpeed;

    private AudioSource[] _audioSources = new AudioSource[3];


    public override void FirstTimeCreated()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_audioManager.CheckAvailability(this.GetType().Name + "Sound0" + (i + 1)))
                _audioSources[i] = _audioManager.FindAudioSourceByClipName(this.GetType().Name + "Sound0" + (i + 1));
            else
                Debug.LogError("Не удалось добавить в _audioSources[" + i + "] объект: " + this.GetType().Name + "Sound0" + (i + 1));

        }
    }

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    IEnumerator Effectprocess()
    {
        int number = Mathf.RoundToInt(GetSkillValue(Skill.Number));
        Enemy[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, number);
        //Debug.Log(number + "  " + nearestEnemies.Length);
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 position = _player.transform.position;
                ChainLightning chainLightning = Instantiate(_chainLightningPrefab, position, Quaternion.identity);

                _audioSource.Play();

                chainLightning.Init(nearestEnemies[i], GetSkillValue(Skill.Damage), _bulletSpeed, (int)GetSkillValue(Skill.PassCount), _audioSources);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
