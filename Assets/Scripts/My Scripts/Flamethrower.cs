using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    private Transform _target;
    private float _damage;
    private float _piriod;
    private float _timer;

    public void Init(Transform target, float damage, float period)
    {
        _target = target;
        _damage = damage;
        _piriod = period;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }
    private void LateUpdate()
    {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (_timer > _piriod)
        {
            if (other.GetComponent<Enemy>() is Enemy enemy)
            {
                enemy.SetDamage(_damage);
            }
            _timer = 0;
        }

    }
    public void SetRadius(float radius)
    {
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
}
