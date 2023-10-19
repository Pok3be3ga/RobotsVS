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
        // TODO: Убрать магическое число
        if (_toPlayer.magnitude > 32f)
        {
            transform.position += _toPlayer * 1.95f;
        }
        _targetRotation = Quaternion.LookRotation(_toPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationLerp);
        _rigidbody.velocity = transform.forward * speed;
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
