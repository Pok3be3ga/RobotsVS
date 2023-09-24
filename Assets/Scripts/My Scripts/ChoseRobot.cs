using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChoseRobot : MonoBehaviour
{
    public GameObject[] Robots;
    [SerializeField] RigidbodyMove _rigidbodyMove;
    [SerializeField] PlayerHealth _playerHealth;
    public float SpeedRobot1;
    public float SpeedRobot2;
    public float SpeedRobot3;
    public float SpeedRobot4;
    public float SpeedRobot5;
    

    private void Start()
    {

        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[Progress.InstanceProgress.IndexRobot].SetActive(true);

        if (Progress.InstanceProgress.IndexRobot == 0)
        {
            _rigidbodyMove.Speed = SpeedRobot1;
        }
        if (Progress.InstanceProgress.IndexRobot == 1)
        {
            _rigidbodyMove.Speed = SpeedRobot2;
        }
        if (Progress.InstanceProgress.IndexRobot == 2)
        {
            _rigidbodyMove.Speed = SpeedRobot3;
        }
        if (Progress.InstanceProgress.IndexRobot == 3)
        {
            _rigidbodyMove.Speed = SpeedRobot4;

        }
        if (Progress.InstanceProgress.IndexRobot == 4)
        {
            _rigidbodyMove.Speed = SpeedRobot5; ;
        }
    }
}

