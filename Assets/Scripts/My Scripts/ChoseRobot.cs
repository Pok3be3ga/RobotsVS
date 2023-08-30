using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChoseRobot : MonoBehaviour
{
    public GameObject[] Robots;

    private void Start()
    {
        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[ProgressGame.Instance.index].SetActive(true);
    }
}

