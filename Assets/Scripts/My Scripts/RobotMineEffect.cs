using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMineEffect : MonoBehaviour
{
    public GameObject DamagePrefab;
    public float Period = 0.4f;
    public float Timer;

    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > Period)
        {
            Instantiate(DamagePrefab, transform.position, DamagePrefab.transform.rotation);
            Timer = 0;
        }

    }
}
