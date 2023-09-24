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

    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _layerMask;
    private const int _arraySize = 50;

    private Collider[] _colliders = new Collider[_arraySize];
    private Enemy[] _enemiesToDrag = new Enemy[_arraySize];
    Enemy[] _enemy;
    private void Start()
    {
        StartCoroutine(GetEnemiesToDrag());
    }
    public void Init(Transform target, float damage, float period)
    {
        _target = target;
        _damage = damage;
        _piriod = period;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }
    private void LateUpdate()
    {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
    }
    private void OnTriggerStay(Collider other)
    {

        //int numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);
        //for (int i = 0; i < numberOfColliders; i++)
        //{
        //    _enemiesToDrag[i] = _colliders[i].GetComponent<Enemy>();
        //    //_enemiesToDrag[i].SetDamage(_damage);
        //}
        //for (int i = numberOfColliders; i < _arraySize; i++)
        //{
        //    _enemiesToDrag[i] = null;
        //    //_enemiesToDrag[i].SetDamage(_damage);
        //}


        if (_timer > _piriod)
        {
            if (other.GetComponent<Enemy>() is Enemy enemy)
            {
                enemy.SetDamage(_damage);
            }
            _timer = 0;
        }
    }

    private IEnumerator GetEnemiesToDrag()
    {
        while (true)
        {
            int numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);

            for (int i = 0; i < numberOfColliders; i++)
            {
                _enemiesToDrag[i] = _colliders[i].GetComponent<Enemy>();
            }
            for (int i = numberOfColliders; i < _arraySize; i++)
            {
                _enemiesToDrag[i] = null;
            }

            yield return new WaitForSeconds(0.16f);
        }
    }
}
