using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axes : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed;
    private void Start()
    {
        gameObject.transform.parent = null;
    }
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        transform.position = _target.position;
    }

    [SerializeField] Transform _target;

    public void Setup(Transform target)
    {
        _target = target;
    }
   



}
