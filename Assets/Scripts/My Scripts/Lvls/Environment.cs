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
    private float _newRotation;

    private void FixedUpdate()
    {
        if (!spawnArea.bounds.Contains(transform.position))
        {
            _newPosition = transform.position;
            _newRotation = Random.Range(1,360);

            if (transform.position.x < spawnArea.bounds.min.x || transform.position.x > spawnArea.bounds.max.x)
            {
                _newPosition.x = transform.position.x < spawnArea.bounds.min.x ? spawnArea.bounds.max.x : spawnArea.bounds.min.x;                
            }
            else
            {
                _newPosition.z = transform.position.z < spawnArea.bounds.min.z ? spawnArea.bounds.max.z : spawnArea.bounds.min.z;
            }

            transform.position = _newPosition;
            transform.rotation = Quaternion.Euler(0, _newRotation, 0);
        }
    }
}
