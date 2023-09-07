using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;


public class RocketEffect1 : MonoBehaviour
{

    [SerializeField] private Rocket _RocketPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] float _timer;

    public float Damage = 10;
    public int Number = 3;

    public float Period;


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > Period)
        {
            Instantiate(_RocketPrefab, transform.position, Quaternion.identity);
                _timer = 0;
        }
    }
}
