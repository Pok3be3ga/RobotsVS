using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    private Transform _target;
    private float _damage;
    private float _piriod;
    private float _timer;

    [SerializeField] private LayerMask _layerMask;
    public List<Enemy> _enemyList;
    public void Init(Transform target, float damage, float period)
    {
        _target = target;
        _damage = damage;
        _piriod = period;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i] == null)
            {
                _enemyList.RemoveAt(i);
            }
        }
        
    }
    private void LateUpdate()
    {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
    }
    private void OnTriggerStay(Collider other)
    {
        if (_timer > _piriod)
        {
            for (int i = 0; i < _enemyList.Count; i++)
            {
                _enemyList[i].SetDamage(_damage);
            }
            _timer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            _enemyList.Add(other.GetComponent<Enemy>());

        }
    }
    private void OnTriggerExit(Collider other)
    {
        _enemyList.Remove(other.GetComponent<Enemy>());
    }
    
}
