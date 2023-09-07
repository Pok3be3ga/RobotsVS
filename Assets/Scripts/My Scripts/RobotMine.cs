using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMine : MonoBehaviour
{
    public float Damage;
    public float Cooldown = 0.3f;
    private float _timer;

    public float DestroyTime = 2;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (_timer > Cooldown)
        {
            if (other.GetComponent<Enemy>() is Enemy enemy)
            {
                enemy.SetDamage(Damage, true);
                _timer = 0;

            }
        }

    }
}
