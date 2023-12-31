using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LootCreator : MonoBehaviour
{

    [SerializeField] private float _period = 30f;
    [SerializeField] private Loot _loot;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] float _creationDistance;
    [SerializeField] private float _timer;
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        if (Time.timeScale < 1f) _timer = _timer;
        _timer += Time.deltaTime;
        if (_timer > _period)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 position = _playerTransform.position + new Vector3(randomPoint.x, 0f, randomPoint.y) * _creationDistance;
            Loot newLoot = Instantiate(_loot, position, Quaternion.identity);
            newLoot._audioSource = _audioSource;
            _timer = 0f;
        }
    }

#if (UNITY_EDITOR)
    private void OnDrawGizmosSelected()
    {
        if (_playerTransform)
        {

            Handles.color = Color.green;
            Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _creationDistance);
        }
    }
#endif

}
