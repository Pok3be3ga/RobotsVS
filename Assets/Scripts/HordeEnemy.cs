using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEnemy : Enemy
{
    [SerializeField] private Rigidbody _hordeEnemyRigidbody;

    void FixedUpdate()
    {
        _hordeEnemyRigidbody.velocity = transform.forward * speed;
        _toPlayer = _playerTransform.position - transform.position;

        if (_toPlayer.magnitude < 5f)
        { 
            _targetRotation = Quaternion.LookRotation(_toPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                _targetRotation, Time.deltaTime * _rotationLerp);
            _rigidbody.velocity = transform.forward * speed;
        }
        if (_toPlayer.magnitude > 37f)
        {
            SetActive(false);
        }
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
