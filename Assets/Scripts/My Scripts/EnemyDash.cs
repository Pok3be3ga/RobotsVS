using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyDash : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private float initialSpeed;
    private float targetSpeed = 5f;
    private float accelerationDuration = 0.5f;
    private float decelerationDuration = 0.5f;
    private bool _previousDashIsOver = true;

    private float _elapsedTime;
    private float _decelerationElapsedTime;
    private float _currentScaleY;
    private Vector3 _newScale;

    private void Start()
    {
        initialSpeed = _enemy.speed;
        _newScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (_previousDashIsOver)
        StartCoroutine(ChangeSpeedSmoothly());
    }

    private IEnumerator ChangeSpeedSmoothly()
    {
        _previousDashIsOver = false;

        _elapsedTime = 0f;
        while (_elapsedTime < accelerationDuration)
        {
            _enemy.speed = Mathf.Lerp(initialSpeed, targetSpeed, _elapsedTime / accelerationDuration);

            _currentScaleY = Mathf.Lerp(1f, 0.7f, _elapsedTime / accelerationDuration);
            _newScale.y = _currentScaleY;
            transform.localScale = _newScale;

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        _enemy.speed = targetSpeed;

        yield return new WaitForSeconds(0.5f);

        _decelerationElapsedTime = 0f;
        while (_decelerationElapsedTime < decelerationDuration)
        {
            _enemy.speed = Mathf.Lerp(targetSpeed, initialSpeed, _decelerationElapsedTime / decelerationDuration);

            _currentScaleY = Mathf.Lerp(0.7f, 1f, _decelerationElapsedTime / decelerationDuration);
            _newScale.y = _currentScaleY;
            transform.localScale = _newScale;

            _decelerationElapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        _enemy.speed = initialSpeed;
        yield return new WaitForSeconds(2f);
        _previousDashIsOver = true;
    }
}
