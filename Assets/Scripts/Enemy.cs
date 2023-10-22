using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public Color ColumnColor;

    [SerializeField] protected float _health = 50;
    [SerializeField] protected Rigidbody _rigidbody;
    protected Transform _playerTransform;
    public float speed = 3f;
    private float _startSpeed;
    protected PlayerHealth _playerHealth;
    protected float _attackTimer;
    [SerializeField] private float _attackPeriod = 1f;
    [SerializeField] private float _dps;

    protected EnemyManager _enemyManager;

    [SerializeField] protected EnemyHit _enemyHitPrefab;
    [SerializeField] protected GameObject _deathEffect;
    protected EnemyHit _enemyHit;

    public Action OnTakeDamdage;
    protected bool _isFrozen;
    protected float _rotationLerp = 3f;
    protected Vector3 _toPlayer;
    protected Quaternion _targetRotation;

    public TextMeshPro CoeffHEalthInput;
    public float CoeffHEalth = 1.2f;

    public DelayForSound _enemyHitSound;
    public DelayForSound _enemyDeathSound;

    public void Init(Transform playerTransform, EnemyManager enemyManager,
        int level, int Chapter, DelayForSound enemyDeathSound, DelayForSound enemyHitSound)
    {
        _enemyDeathSound = enemyDeathSound;
        _enemyHitSound = enemyHitSound;

        _startSpeed = speed;
        _playerTransform = playerTransform;
        _attackTimer = 0f;
        _enemyManager = enemyManager;
        _health += Chapter * CoeffHEalth;
        _enemyHit = Instantiate(_enemyHitPrefab, transform.position, Quaternion.identity);
        _enemyHit.Init();
    }

    public void Init(Transform playerTransform, DelayForSound enemyDeathSound, DelayForSound enemyHitSound)
    {
        _enemyDeathSound = enemyDeathSound;
        _enemyHitSound = enemyHitSound;

        _startSpeed = speed;
        _playerTransform = playerTransform;
        _attackTimer = 0f;
        _enemyHit = Instantiate(_enemyHitPrefab, transform.position, Quaternion.identity);
        _enemyHit.Init();
    }

    void Update()
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
        _toPlayer = _playerTransform.position - transform.position;
        // TODO: ”брать магическое число
        if (_toPlayer.magnitude > 32f)
        {
            transform.position += _toPlayer * 1.95f;
        }
        _targetRotation = Quaternion.LookRotation(_toPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationLerp);
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
        speed /= 2;
        _enemyHit.ShowDamage(transform.position, value);
        OnTakeDamdage.Invoke();

        if (_health <= 0)
        {
            Die();
        }
        else
        {
            _enemyHitSound.PlaySound();
        }

        Freez(freezTime);
        StartCoroutine(ReturnSpeedToDefaultValue(1));
    }

    public void Die()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);

        _enemyDeathSound.PlaySound();

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
        yield return new WaitForSeconds(3);
        _isFrozen = false;
    }
    
    private IEnumerator ReturnSpeedToDefaultValue(float period)
    {
        yield return new WaitForSeconds(period);
        speed = _startSpeed;
    }
}
