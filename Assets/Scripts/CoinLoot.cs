using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLoot : Loot
{

    [SerializeField] private int _value = 5;
    
    private void Awake()
    {
        LootType = LootType.Coin;
        _value += Progress.InstanceProgress.ProgressData.LootLevel;
    }

    protected override void Take(Collector coinCollector)
    {
        base.Take(coinCollector);
        coinCollector.CollectCoin(_value);
    }

}
