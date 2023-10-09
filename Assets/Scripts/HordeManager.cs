using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class HordeManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minDelay;

    [SerializeField] private int _hordeMembers;
    [SerializeField] private HordeEnemy _hordeEnemyPrefab;
    [SerializeField] private HordeEnemy[] _hordeArray;
    [SerializeField] private float _delayBeforeAttack = 10;

    private float _spawnRange = 1.7f;
    private int _playerLevel;
    private EnemyManager _enemyManager;
    private Vector3 _positionInFormation;
    private Vector3 _defaultPositionForHorde;
    private Quaternion _defaultRotationForHorde;


    public void Init(Transform playerTransform, EnemyManager enemyManager, ref int level)
    {
        _playerTransform = playerTransform;
        _playerLevel = level;
        _enemyManager = enemyManager;
        _hordeArray = new HordeEnemy[_hordeMembers];
        _defaultPositionForHorde = transform.position;
        _defaultRotationForHorde = transform.rotation;

        CreateHorde(_hordeEnemyPrefab);
        SetActiveForHorde(false);
    }

    public void FixedUpdate()
    {
        _delayBeforeAttack -= Time.deltaTime;
        if (_delayBeforeAttack <= 0)
        {
            _delayBeforeAttack = Random.Range(_minDelay, _maxDelay) - (_playerLevel * 2);

            StartCoroutine(HordeAttack(_hordeEnemyPrefab));
        }
    }

    public void CreateHorde(HordeEnemy _hordeEnemy)
    {
        _positionInFormation = new Vector3(-_hordeMembers / 2, -1f, 0f);

        for (int i = 0; i < _hordeMembers; i++)
        {
            if (_hordeArray[i] == null)
            {
                _hordeEnemyPrefab = Instantiate(_hordeEnemy, _positionInFormation, Quaternion.identity, transform);
                _hordeArray[i] = _hordeEnemyPrefab;
                _hordeEnemyPrefab.Init(_playerTransform);
            }
            else
            {
                _hordeArray[i].transform.SetPositionAndRotation(_positionInFormation, _defaultRotationForHorde);
            }
            _positionInFormation.x += _spawnRange;
        }
    }

    private IEnumerator HordeAttack(HordeEnemy _hordeEnemy)
    {
        SetActiveForHorde(true);
        transform.position = _enemyManager.RandomSpawnPosition() + _playerTransform.position;
        transform.LookAt(_playerTransform, Vector3.up);
        yield return new WaitForSeconds(3f);
        SetActiveForHorde(false);
        transform.SetPositionAndRotation(_defaultPositionForHorde, _defaultRotationForHorde);
        CreateHorde(_hordeEnemy);
    }

    private void SetActiveForHorde(bool isActive)
    {
        foreach (Enemy hordeEnemy in _hordeArray)
        {
            hordeEnemy.SetActive(isActive);
        }
    }
}
