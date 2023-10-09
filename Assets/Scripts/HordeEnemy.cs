using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEnemy : Enemy
{
    [SerializeField] private Rigidbody _hordeEnemyRigidbody;
    private bool _isFrozen;

    void FixedUpdate()
    {
        if (_isFrozen) return;

        _hordeEnemyRigidbody.velocity = transform.forward * speed;
    }
}
