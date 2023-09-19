using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] EffectsManager EffectsManager;
    [SerializeField] EnemyManager EnemyManager;
    [SerializeField] CoinCounter CoinCounter;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CoinCounter.AddCoins(10);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            EffectsManager.ShowCards(2);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemyManager.OnLastKilled();
        }
    }
}
