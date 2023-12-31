using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType
{
    Experience,
    Coin,
    Health,
    Magnet
}

public class Loot : MonoBehaviour
{
    public LootType LootType { get; protected set; }
    [SerializeField] protected float _moveSpeed = 18f;
    [SerializeField] protected float _rotationSpeed = 360f;
    [SerializeField] protected Collider _collider;
    [SerializeField] public AudioSource _audioSource;
    protected CoinCounter coinCounter;


    protected void LateUpdate()
    {
        transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f);
    }

    public void Collect(Collector coinCollector)
    {
        _collider.enabled = false;
        StartCoroutine(MoveToPlayer(coinCollector));
    }

    protected IEnumerator MoveToPlayer(Collector coinCollector)
    {
        float t = 0f;
        Vector3 toTarget = (coinCollector.transform.position - transform.position).normalized;
        Vector3 a = transform.position;
        Vector3 b = a + Vector3.up * 1f - toTarget * 2.5f;
        Vector3 moveing = transform.position;
        while (true)
        {
            t += Time.deltaTime * 2.5f;

            moveing = Vector3.MoveTowards(moveing, coinCollector.transform.position, Time.deltaTime * _moveSpeed);

            Vector3 c = moveing + Vector3.up * 2f;
            Vector3 d = moveing;

            Vector3 bezier = Bezier.GetPoint(a, b, c, d, t);

            transform.position = bezier;
            float distance = Vector3.Distance(transform.position, coinCollector.transform.position);
            if (distance < 0.1)
            {
                Take(coinCollector);
                break;
            }
            yield return null;
        }
    }

    protected virtual void Take(Collector coinCollector)
    {
        if (_audioSource != null)
            _audioSource.Play();

        Die();
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
