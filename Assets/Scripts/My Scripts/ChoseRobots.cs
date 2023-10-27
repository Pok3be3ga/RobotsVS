using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoseRobots : MonoBehaviour
{
    [SerializeField] int index;
    public GameObject[] Robots;
    public GameObject[] Button;

    private void Start()
    {
        index = Progress.InstanceProgress.IndexRobot;
        Robots[index].SetActive(true);
        //Button[index].SetActive(true);
    }
    public void ChoseRobotsDawn()
    {
        
        index -=1;
        if (index < 0)
        {
            index = Button.Length - 1;
        }
        for (int i = 0; i < Button.Length; i++)
        {
            Button[i].SetActive(false);
        }
        Button[index].SetActive(true);
        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[index].SetActive(true);
        SaveRobotIndex();


    }
    public void ChoseRobotsUp()
    {
        index += 1;
        if (index > Robots.Length - 1)
        {
            index = 0;
        }
        for (int i = 0; i < Button.Length; i++)
        {
            Button[i].SetActive(false);
        }
        Button[index].SetActive(true);
        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[index].SetActive(true);
        SaveRobotIndex();
    }

    public void SaveRobotIndex()
    {
        Progress.InstanceProgress.IndexRobot = index;
    }


}
