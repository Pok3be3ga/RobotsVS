using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CoinLoot : Loot
{

    [SerializeField] private int _value = 5;
    
    private void Awake()
    {
        LootType = LootType.Coin;
        _value += YandexGame.savesData.LootLevel;
    }

    protected override void Take(Collector coinCollector)
    {
        base.Take(coinCollector);
        coinCollector.CollectCoin(_value);
    }

}
