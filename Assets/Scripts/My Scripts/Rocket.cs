using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Rocket : MonoBehaviour
{
    private Enemy _targetEnemy;
    private float _speed;
    private float _damage;
    private float _timer;

    public void Init(Enemy targetEnemy, float damage, float speed)
    {
        _damage = damage;
        _targetEnemy = targetEnemy;
        _speed = speed;
        Destroy(gameObject, 4f);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer < 0.5) {
            transform.position += Vector3.up * 0.1f;
        }
        else
        {
            if (_targetEnemy)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, _speed * Time.deltaTime);
                Vector3 newDir = Vector3.RotateTowards(transform.forward, (_targetEnemy.transform.position - transform.position), 360f, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);
                if (transform.position == _targetEnemy.transform.position)
                {
                    AffectEnemy();
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        
    }

    void AffectEnemy()
    {
        _targetEnemy.SetDamage(_damage, true);
    }
}
