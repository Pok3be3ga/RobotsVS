using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PermanentProgress : MonoBehaviour
{

    private Progress _progress;

    [SerializeField] private PermanentProgressCard _healthCard;
    [SerializeField] private PermanentProgressCard _damageCard;
    [SerializeField] private PermanentProgressCard _lootCard;

    [SerializeField] private Player _player;

    private CoinCounter _coinCounter;

    public void Init(Progress progress, CoinCounter coinCounter) {
        _progress = progress;
        _coinCounter = coinCounter;
        _healthCard.Init(progress, AddHealth, YandexGame.savesData.HealthLevel);
        _damageCard.Init(progress, AddDamage, YandexGame.savesData.DamageLevel);
        _lootCard.Init(progress, AddLoot, YandexGame.savesData.LootLevel);
    }

    public void AddHealth(int price)
    {
        YandexGame.savesData.HealthLevel += 1;
        _coinCounter.SpendCoins(price);
        UpdateCards();
        YandexGame.SaveProgress();
    }

    public void AddDamage(int price)
    {
        YandexGame.savesData.DamageLevel += 1;
        _coinCounter.SpendCoins(price);
        UpdateCards();
        YandexGame.SaveProgress();
    }

    public void AddLoot(int price)
    {
        YandexGame.savesData.LootLevel += 1;
        _coinCounter.SpendCoins(price);
        UpdateCards();
        YandexGame.SaveProgress();
    }

    private void UpdateCards() {
        _healthCard.SetLevel(YandexGame.savesData.HealthLevel);
        _damageCard.SetLevel(YandexGame.savesData.DamageLevel);
        _lootCard.SetLevel(YandexGame.savesData.LootLevel);
    }

    public float GetHealth() { 
        return _healthCard.PercentPerLevel * YandexGame.savesData.HealthLevel * 0.01f;
    }

    public float GetDamage()
    {
        return _damageCard.PercentPerLevel * YandexGame.savesData.DamageLevel * 0.01f;
    }

    public float GetLoot() { 
        return _lootCard.PercentPerLevel * YandexGame.savesData.LootLevel * 0.01f;
    }

}
