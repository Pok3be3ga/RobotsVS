using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Environment _environment;
    [SerializeField] private Renderer _spawnArea;

    private string _resourcePath;
    [SerializeField] private Renderer _ground;
    private Material _newGround;
    [SerializeField] int LvlNumber;
    [SerializeField] private Environment[] _loadedObjects;

    private int _objectCount;
    private float _spawnAreaSizeZ;
    private float _spawnAreaSizeX;
    private int _copiesOfOneObject;
    [SerializeField][Range(0f, 1f)] public float density;
    Vector3 spawnPosition;
    float objectRadius;

    public bool spawnMore = false;
    private void Update()
    {
        if (spawnMore)
        {
            SpawnEnvironment();
            spawnMore = false;
        }
    }

    public void Init()
    {
        LvlNumber = Progress.InstanceProgress.ProgressData.NumberOfEnvironment + 1;
        _spawnAreaSizeZ = _spawnArea.bounds.size.z;
        _spawnAreaSizeX = _spawnArea.bounds.size.x;
        LoadNewGround();
        ChangeGround();
        LoadObjects();        
        SpawnEnvironment();
    }

    public void LoadNewGround()
    {
        _resourcePath = "Lvl " + LvlNumber + "/Ground";
        _newGround = Resources.Load<Material>(_resourcePath);
    }

    public void ChangeGround()
    {
        if (_newGround != null)
        {
            _ground.material = _newGround;
        }
        else
        {
            Debug.LogError("Не удалось загрузить Ground для уровня " + LvlNumber);
        }
    }

    public void LoadObjects()
    {
        _resourcePath = "Lvl " + LvlNumber;
        _loadedObjects = Resources.LoadAll<Environment>(_resourcePath);
        if (_loadedObjects.Length == 0)
        {
            Debug.LogWarning("Не удалось найти объекты в ресурсах для уровня " + LvlNumber);
        }
        else
        {
            Debug.Log("Загружено " + _loadedObjects.Length + " объектов из ресурсов для уровня " + LvlNumber);
        }
    }

    private void SpawnEnvironment()
    {
        _objectCount = Mathf.RoundToInt((_spawnAreaSizeZ * _spawnAreaSizeX / 100) * density);
        _copiesOfOneObject = _objectCount / _loadedObjects.Length;
        for (int i = 0; i < _loadedObjects.Length; i++)
        {
            if (_loadedObjects[i].TryGetComponent<SphereCollider>(out _))
            {
                objectRadius = _loadedObjects[i].GetComponent<SphereCollider>().radius;
            }
            else
            {
                objectRadius = 1;
            }
            for (int j = 0; j < _copiesOfOneObject; j++)
            {
                do
                {
                    spawnPosition = new Vector3(Random.Range(-_spawnAreaSizeZ / 2, _spawnAreaSizeZ / 2), 0, Random.Range(-_spawnAreaSizeX / 2, _spawnAreaSizeX / 2));
                } while (Physics.CheckSphere(spawnPosition, objectRadius, LayerMask.GetMask("Ground")));

                Environment spawnedObject = Instantiate(_loadedObjects[i], spawnPosition, Quaternion.Euler(0, spawnPosition.x * 3, 0));

                spawnedObject.spawnArea = _spawnArea;
                spawnedObject.spawnAreaSizeZ = _spawnAreaSizeZ;
                spawnedObject.spawnAreaSizeX = _spawnAreaSizeX;
                spawnedObject.transform.parent = transform;
            }
        }        
    }
}

