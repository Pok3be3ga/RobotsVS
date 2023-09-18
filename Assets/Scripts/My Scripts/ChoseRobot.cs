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
    

    private void Start()
    {

        for (int i = 0; i < Robots.Length; i++)
        {
            Robots[i].SetActive(false);
        }
        Robots[Progress.InstanceProgress.IndexRobot].SetActive(true);

        if (Progress.InstanceProgress.IndexRobot == 0)
        {
            _rigidbodyMove.Speed = 4;
        }
        if (Progress.InstanceProgress.IndexRobot == 1)
        {
            _rigidbodyMove.Speed = 5;
        }
        if (Progress.InstanceProgress.IndexRobot == 2)
        {
            _rigidbodyMove.Speed = 8;
        }
        if (Progress.InstanceProgress.IndexRobot == 3)
        {
            _rigidbodyMove.Speed = 6;

        }
        if (Progress.InstanceProgress.IndexRobot == 4)
        {
            _rigidbodyMove.Speed = 5;;
        }
    }
}

