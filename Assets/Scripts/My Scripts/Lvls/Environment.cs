using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Environment : MonoBehaviour
{
    public Renderer spawnArea;
    public float spawnAreaSizeZ;
    public float spawnAreaSizeX;

    private Vector3 _newPosition;
    private float _randomValue;

    private void FixedUpdate()
    {
        if (!spawnArea.bounds.Contains(transform.position))
        {
            _newPosition = transform.position;
            _randomValue = Random.Range(-180,180);

            if (transform.position.x < spawnArea.bounds.min.x || transform.position.x > spawnArea.bounds.max.x)
            {
                _newPosition.x = transform.position.x < spawnArea.bounds.min.x ? spawnArea.bounds.max.x : spawnArea.bounds.min.x;
                _newPosition.z += _randomValue / 20;
            }
            else
            {
                _newPosition.x += _randomValue / 20;
                _newPosition.z = transform.position.z < spawnArea.bounds.min.z ? spawnArea.bounds.max.z : spawnArea.bounds.min.z;
            }

            transform.position = _newPosition;
            transform.rotation = Quaternion.Euler(0, _randomValue, 0);
        }
    }
}
