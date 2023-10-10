using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEnemy : Enemy
{
    [SerializeField] private Rigidbody _hordeEnemyRigidbody;

    void FixedUpdate()
    {
        _hordeEnemyRigidbody.velocity = transform.forward * speed;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
