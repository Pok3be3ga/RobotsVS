using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class HordeManager : MonoBehaviour
{
    [SerializeField] private int _hordeMembers;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minDelay;
    private float _delay;
    private int _numberOfAttacks;
    private int _playerLevel;
    private EnemyManager _enemyManager;

    private List<Enemy> _hordeList = new List<Enemy>();

    [SerializeField] private Enemy _hordeEnemyPrefab;

    public void Init(Transform playerTransform, EnemyManager enemyManager, ref int level)
    {
        _playerTransform = playerTransform;
        _playerLevel = level;
        _enemyManager = enemyManager;
        _delay = Random.Range(_minDelay, _maxDelay);
        CreateHorde(_hordeEnemyPrefab);
    }

    public void FixedUpdate()
    {
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            _numberOfAttacks = _playerLevel + 1;
            ActivateHordeAttack(_numberOfAttacks);
            _delay = Random.Range(_minDelay, _maxDelay);
        }
    }

    public void CreateHorde(Enemy enemy)
    {
        Vector3 startPosition = new Vector3(-_hordeMembers / 2, -1f, 0f);
        Enemy newHordeEnemy;
        for (int i = 0; i < _hordeMembers; i++)
        {
            newHordeEnemy = Instantiate(enemy, startPosition, Quaternion.identity, transform);
            newHordeEnemy.SetActive(false);
            _hordeList.Add(newHordeEnemy);
            newHordeEnemy.Init(_playerTransform);
            startPosition.x += 1.7f;
        }
    }

    public void ActivateHordeAttack(int numberOfAttacks)
    {
        Vector3 defaultPosition = transform.position;
        for (int i = 0; i < 1; i++)
        {
            transform.position = _enemyManager.RandomSpawnPosition() + _playerTransform.position;
            transform.LookAt(_playerTransform, Vector3.up);

            foreach (Enemy hordeEnemy in _hordeList)
            {
                hordeEnemy.SetActive(true);
            }


        }
        //_horde.position = defaultPosition;
    }
}
