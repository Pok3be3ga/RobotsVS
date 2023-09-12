using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Axes : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _damage;
    [SerializeField] DamageOnTrigger damageOnTrigger;
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _number;
    public void Init(float rotationSpeed, float damage, Transform target)
    {
        _rotationSpeed = rotationSpeed;
        _damage = damage;
        _playerTransform = target;
        damageOnTrigger.Init(damage);
    }


    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        transform.position = _playerTransform.position;
    }

   



}
