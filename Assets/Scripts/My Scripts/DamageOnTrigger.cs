using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public float Damage = 5;
    private void OnTriggerEnter(Collider other)
    {
            other.GetComponent<Enemy>().SetDamage(Damage);
    }
}
