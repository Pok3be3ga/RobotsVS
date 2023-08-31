using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    public float _damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() is Enemy enemy)
        {
            enemy.SetDamage(_damage, true);

        }
    }      
}
