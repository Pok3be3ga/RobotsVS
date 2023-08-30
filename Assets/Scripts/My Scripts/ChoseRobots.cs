using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoseRobots : MonoBehaviour
{
    [SerializeField] int index;
    public GameObject[] Robots;

    private void Start()
    {
        index = ProgressGame.Instance.index;
        Robots[index].SetActive(true);
    }
    public void ChoseRobotsDawn()
    {
        
        index -=1;
        if (index < 0)
        {
            index = 0;
        }
        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[index].SetActive(true);
        

    }
    public void ChoseRobotsUp()
    {
        index += 1;
        if (index > Robots.Length - 1)
        {
            index = Robots.Length - 1;
        }
        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[index].SetActive(true);
        
    }

    public void SaveRobotIndex()
    {
        ProgressGame.Instance.index = index;
    }


}
