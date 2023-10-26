using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;

public class HordeManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minDelay;  
    [SerializeField] private int _hordeMembers;
    [SerializeField] private HordeEnemy _hordeEnemyPrefab;
    [SerializeField] private HordeEnemy[] _hordeArray;
    [SerializeField] private float _delayBeforeAttack;

    private float _spawnRange;
    private EnemyManager _enemyManager;
    private Vector3 _positionInFormation;
    private Vector3 _defaultPositionForHorde;
    private Quaternion _defaultRotationForHorde;
    private bool HordeAttackActivated = false; 
    private int _valueForDelay = -1;
    private int _valueForAttacksNumber = 1;
    [SerializeField] private DelayForSound _enemyDeathSound;
    [SerializeField] private DelayForSound _enemyHitSound;


    public void Init(EnemyManager enemyManager, DelayForSound enemyDeathSound, DelayForSound enemyHitSound)
    {
        _enemyDeathSound = enemyDeathSound;
        _enemyHitSound = enemyHitSound;
        _enemyManager = enemyManager;
        _hordeArray = new HordeEnemy[_hordeMembers];
        _defaultPositionForHorde = transform.position;
        _defaultRotationForHorde = transform.rotation;
        _spawnRange = 2* _hordeEnemyPrefab.GetComponent<SphereCollider>().radius;
        _delayBeforeAttack = _maxDelay;
        CreateHorde(_hordeEnemyPrefab);
    }

    public void FixedUpdate()
    {
        _delayBeforeAttack -= Time.deltaTime;
        if (_delayBeforeAttack <= 0 && !HordeAttackActivated)
        {            
            ActivateHordeAttack(_hordeEnemyPrefab);
        }
        if (_delayBeforeAttack < -9)
        {
            _delayBeforeAttack = _minDelay;

            _valueForDelay++;
            if (_valueForDelay > _valueForAttacksNumber)
            {
                _valueForAttacksNumber++;
                _valueForDelay = 0;
                _delayBeforeAttack = _maxDelay;
            }
        }
    }

    public void CreateHorde(HordeEnemy _hordeEnemy)
    {
        _positionInFormation = new Vector3(-_hordeMembers / 2f, 0, 0);

        for (int i = 0; i < _hordeMembers; i++)
        {
            if (_hordeArray[i] == null)
            {
                HordeEnemy newHordeEnemy = Instantiate(_hordeEnemy, _positionInFormation, Quaternion.identity, transform);
                _hordeArray[i] = newHordeEnemy;
                newHordeEnemy.Init(_playerTransform, _enemyDeathSound, _enemyHitSound);
                SetActiveForHorde(false);
            }
            else
            {
                _hordeArray[i].transform.SetPositionAndRotation(_positionInFormation, _defaultRotationForHorde);
            }
            _positionInFormation.x += _spawnRange;

            if (i == (_hordeMembers / 2) - 1)
            {
                _positionInFormation.z -= _spawnRange;
                _positionInFormation.x = (-_hordeMembers / 2) - (_spawnRange / 2);
            }
        }
    }

    private void ActivateHordeAttack(HordeEnemy _hordeEnemy)
    {   
        transform.position = _enemyManager.RandomSpawnPosition() + _playerTransform.position;
        transform.LookAt(_playerTransform, Vector3.up);
        SetActiveForHorde(true);
        HordeAttackActivated = true;
    }

    private void DeactivateHordeAttack(HordeEnemy _hordeEnemy)
    {
        SetActiveForHorde(false);
        transform.SetPositionAndRotation(_defaultPositionForHorde, _defaultRotationForHorde);
        CreateHorde(_hordeEnemy);
        HordeAttackActivated = false;
    }

    private void SetActiveForHorde(bool isActive)
    {
        foreach (HordeEnemy hordeEnemy in _hordeArray)
        {
            if (hordeEnemy != null)
            {
                hordeEnemy.SetActive(isActive);
            }
        }
    }
}
