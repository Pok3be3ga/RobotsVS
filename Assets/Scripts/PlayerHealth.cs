using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerHealth : MonoBehaviour
{
    public float StartMaxHealth = 100f;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    [SerializeField] private Player _player;
    public Action<float, float, bool> OnChangeHealth;
    public Action OnDie;
    private float _timer;
    public float RegenerationPeriod;

    private List<IPlayerHealthEffect> _playerHealthEffects = new List<IPlayerHealthEffect>();

    private GameStateManager _gameStateManager;

    public AudioSource DamageAudio;

    public void AddHealthEffect(IPlayerHealthEffect effect)
    {
        _playerHealthEffects.Add(effect);
    }

    public void Init(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _maxHealth = GetMaxHealth() + YandexGame.savesData.HealthLevel * 10;
        SetHealth(_maxHealth, false);
    }

    // возвращает максимально здоровье с учетом постоянной прокачки
    public float GetMaxHealth()
    {
        float result = StartMaxHealth * (1 + _player.MaxHpBoost);
        Debug.Log(result);
        return result;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > RegenerationPeriod)
        {
            _timer = 0f;
            Regenerate();
        }
    }

    public void Regenerate()
    {
        if (_currentHealth == _maxHealth) return;
        float newHealth = _currentHealth * (1f + _player.HealthRegeneration);
        newHealth = Mathf.Min(newHealth, _maxHealth);
        SetHealth(newHealth, false);
    }

    public void SetDamage(float value)
    {

        DamageAudio.Play();
        foreach (var item in _playerHealthEffects)
        {
            item.OnSetDamage(ref value);
        }

        float newHealth = _currentHealth - value * (1 - _player.DamageReduction);
        newHealth = Mathf.Max(newHealth, 0);
        SetHealth(newHealth, true);
        if (newHealth == 0)
        {
            Die();
        }
    }

    public void SetMaxHealth()
    {
        SetHealth(_maxHealth, false);
    }

    public void SetHealth(float value, bool isDamage)
    {
        _currentHealth = value;
        Debug.Log("SetHealth " + _currentHealth + "  " + _maxHealth);
        OnChangeHealth?.Invoke(_currentHealth, _maxHealth, isDamage);

    }

    public void BoostMaxHp(float percent)
    {
        float maxHealthBefore = _maxHealth;
        _maxHealth = GetMaxHealth() * (1 + percent);
        float difference = _maxHealth / maxHealthBefore;
        _currentHealth *= difference;
        SetHealth(_currentHealth, false);
    }

    public void Die()
    {
        //Time.timeScale = 0f;
        _gameStateManager.SetLose();
        // OnDie.Invoke();
    }

    public void GetHealed()
    {
        if (_currentHealth <= _maxHealth)
            _currentHealth += _maxHealth / 2;

        else
            _currentHealth = _maxHealth;
    }

}
