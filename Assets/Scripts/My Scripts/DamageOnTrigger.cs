using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            other.GetComponent<Enemy>().SetDamage(10);
    }
}
