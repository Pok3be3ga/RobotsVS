using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [field: SerializeField] public Color ColumnColor;

    [SerializeField] private float _health = 50;
    private Transform _playerTransform;
    [SerializeField] Rigidbody _rigidbody;
    public float speed = 3f;
    private PlayerHealth _playerHealth;
    private float _attackTimer;
    [SerializeField] private float _attackPeriod = 1f;
    [SerializeField] private float _dps;

    private EnemyManager _enemyManager;

    [SerializeField] private EnemyHit _enemyHitPrefab;
    [SerializeField] private GameObject _deathEffect;
    private EnemyHit _enemyHit;

    public Action OnTakeDamdage;
    private bool _isFrozen;
    public float _rotationLerp = 3f;

    private bool isActive;

    public void Init(Transform playerTransform, EnemyManager enemyManager, int level)
    {
        _playerTransform = playerTransform;
        _attackTimer = 0f;
        _enemyManager = enemyManager;
        _health += level * 2;
        _enemyHit = Instantiate(_enemyHitPrefab, transform.position, Quaternion.identity);
        _enemyHit.Init();
    }
    
    public void Init(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _attackTimer = 0f;
        _enemyHit = Instantiate(_enemyHitPrefab, transform.position, Quaternion.identity);
        _enemyHit.Init();
    }

    private void Update()
    {
        if (_playerHealth)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _attackPeriod)
            {
                _attackTimer = 0f;
                _playerHealth.SetDamage(_dps * _attackPeriod);
            }
        }

    }

    void FixedUpdate()
    {
        //return;
        if (_playerTransform == null) return;
        if (_isFrozen) return;
        Vector3 toPlayer = _playerTransform.position - transform.position;
        // TODO: ”брать магическое число
        if (toPlayer.magnitude > 32f)
        {
            transform.position += toPlayer * 1.95f;
        }
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationLerp);
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
            if (other.attachedRigidbody.GetComponent<PlayerHealth>() is PlayerHealth playerHealth)
                _playerHealth = playerHealth;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody)
            if (other.attachedRigidbody.GetComponent<PlayerHealth>())
            {
                _attackTimer = 0f;
                _playerHealth = null;
            }
    }

    public void SetDamage(float value)
    {
        SetDamage(value, 0f);
    }

    public void SetDamage(float value, bool freez = false)
    {
        SetDamage(value, freez ? 0.2f : 0);
    }

    public void SetDamage(float value, float freezTime)
    {
        _health -= value;
        _enemyHit.ShowDamage(transform.position, value);
        OnTakeDamdage.Invoke();
        if (_health <= 0)
        {
            Die();
        }
        Freez(freezTime);
    }

    public void Die()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);

        // ѕроверка дл€ тестовых врагов, которые были созданы не enemyManager-ом
        if (_enemyManager)
        {
            _enemyManager.ExcludeDead(this);
        }
        Destroy(gameObject);
        Destroy(_enemyHit.gameObject);
    }

    public void Drag(Vector3 velocity, float time)
    {
        _rigidbody.velocity += velocity * time;
    }

    //private Coroutine _freezCoroutine;
    private void Freez(float period)
    {
        if (period == 0) return;
        //_freezCoroutine = StartCoroutine(FreezCycle(period));
        StartCoroutine(FreezCycle(period));
    }

    private IEnumerator FreezCycle(float period)
    {
        _isFrozen = true;
        _rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(period);
        _isFrozen = false;
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(active);
            child.gameObject.SetActive(active);
        }
    }
}
