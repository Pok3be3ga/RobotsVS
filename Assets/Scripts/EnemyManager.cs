using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;
    private float _timer;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;
    [SerializeField] private List<Enemy> _enemyiesList = new List<Enemy>();

    public ChapterSettings[] ChapterSettings;

    [SerializeField] private ChapterSettings _chapterSettings;

    [Tooltip("List of enemies for this chapter")]
    private List<Enemy> _enemyList = new List<Enemy>();
    private List<float> _periodList = new List<float>();
    private List<float> _nextTimeToCreateList = new List<float>();
    private GameManager _gameManager;

    // вероятность, что вместо очков опыта выпадет монетка
    private float _coinDropProbability = 0.04f;
    [SerializeField] private Loot _expPrefab;
    [SerializeField] private AudioSource _expTakeSound;
    [SerializeField] private Loot _coinPrefab;
    [SerializeField] private AudioSource _coinTakeSound;
    [SerializeField] public int _numberOfWaves;

    private bool _isWaveLast = false;
    private int _level = 0;
    private GameStateManager _gameStateManager;

    [SerializeField] private DelayForSound _enemyDeathSound;
    [SerializeField] private DelayForSound _enemyHitSound;
    [SerializeField] private AudioSource _winSound01;
    [SerializeField] private AudioSource _gameSound;
    [SerializeField] private TMP_Text _levelProgress;
    // Строчки текста для перевода
    string _textLevel;
    [SerializeField] string _textLevelRu;
    [SerializeField] string _textLevelEn;

    string _textEnemy;
    [SerializeField] string _textEnemyRu;
    [SerializeField] string _textEnemyEn;

    public void Init(GameManager gameManager, GameStateManager gameStateManager)
    {
        _gameManager = gameManager;
        _gameStateManager = gameStateManager;
        _gameManager.OnUpLevel += UpLevel;
        _chapterSettings = ChapterSettings[YandexGame.savesData.NumberOfEnvironment];
        _numberOfWaves = YandexGame.savesData.NumberOfWaves;
        DisplayLevelProgress();
        SetupEnemies();

        if (YandexGame.EnvironmentData.language == "en")
        {
            _textLevel = _textLevelEn;
            _textEnemy = _textEnemyEn;
        }
        else if (YandexGame.EnvironmentData.language == "ru")
        {
            _textLevel = _textLevelRu;
            _textEnemy = _textEnemyRu;
    }
        else
        {
            _textLevel = _textLevelRu;
            _textEnemy = _textEnemyRu;
        }
    }

    private void DisplayLevelProgress()
    {
        if (!_isWaveLast)
            _levelProgress.text = _textLevel + " " + (_numberOfWaves + 1);
        else
            _levelProgress.text = _textEnemy + " " + _enemyiesList.Count;
    }



    private void SetupEnemies()
    {
        // Инициализация тестовых врагов, которые присутствуют в сцене изначально
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    GameObject enemyGameObject = transform.GetChild(i).gameObject;
        //    if (enemyGameObject.activeSelf)
        //    {
        //        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        //        _enemyiesList.Add(enemy);
        //        //enemy.Init(_playerTransform, this, _level);
        //    }
        //}

        _enemyList.Clear();
        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++)
        {
            _enemyList.Add(_chapterSettings.EnemyWavesArray[i].Enemy);
        }
    }

    private void UpLevel(int level)
    {
        int finalLevel;
        if (_numberOfWaves == -1)
        {
            finalLevel = _chapterSettings.EnemyWavesArray[0].NumberPerSecond.Length;
        }
        else
        {
            finalLevel = _numberOfWaves;
        }
        if (level == finalLevel)
        {
            _isWaveLast = true;
        }
        else
        {
            _periodList.Clear();
            _nextTimeToCreateList.Clear();

            // Для каждого из типов врагов обновляем период создания и время создания следующего врага
            for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++)
            {
                _periodList.Add(_chapterSettings.EnemyWavesArray[i].NumberPerSecond[level]);
                _nextTimeToCreateList.Add(_timer + 1f / _periodList[i]);
            }
        }

        _level = level;
        DisplayLevelProgress();
    }

    //private IEnumerator ProcessLastWave() {
    //    while (true) {

    //        yield return null;
    //    }
    //    OnLastKilled();
    //}

    void Update()
    {
        // если волна последняя, то больше никого не создавать
        if (_isWaveLast)
        {
            return;
        }

        _timer += Time.deltaTime;
        for (int i = 0; i < _nextTimeToCreateList.Count; i++)
        {
            if (_timer > _nextTimeToCreateList[i])
            {
                CreteEnemy(_enemyList[i]);
                _nextTimeToCreateList[i] = _timer + 1f / _periodList[i];
            }
        }
    }

    // Создать врага в случайной точке кольца вокруг плеера
    public Enemy CreteEnemy(Enemy enemy)
    {
        Enemy newEnemy = Instantiate(enemy, RandomSpawnPosition() + _playerTransform.position, Quaternion.identity, transform);
        _enemyiesList.Add(newEnemy);
        newEnemy.Init(_playerTransform, this, _level, YandexGame.savesData.Chapter, _enemyDeathSound, _enemyHitSound);
        return newEnemy;
    }

    public Vector3 RandomSpawnPosition()
    {
        // ФУНКЦИЯ ДЛЯ ПОЛУЧЕНИЯ СЛУЧАЙНОЙ ПОЗИЦИИ В КРУГЕ ВОКРУГ ИГРОКА ДЛЯ СПАВНА ВРАГОВ 

        Vector2 randomVector = Random.insideUnitCircle;
        Vector2 randomPoint = randomVector.normalized * Random.Range(_minRadius, _maxRadius);
        Vector3 randomPointXZ = new Vector3(randomPoint.x, 0, randomPoint.y);

        return randomPointXZ;
    }

    public void ExcludeDead(Enemy enemy)
    {
        _enemyiesList.Remove(enemy);

        // Враг создает или очики опыта или монету
        if (Random.value < _coinDropProbability)
        {
            Loot coinObject = Instantiate(_coinPrefab, enemy.transform.position, Quaternion.identity);

            //Instantiate(_coinPrefab, enemy.transform.position, Quaternion.identity);
            coinObject._audioSource = _coinTakeSound;
        }
        else
        {
            Loot expObject = Instantiate(_expPrefab, enemy.transform.position, Quaternion.identity);
            //Instantiate(_expPrefab, enemy.transform.position, Quaternion.identity);
            expObject._audioSource = _expTakeSound;
        }

        if (_isWaveLast)
            if (_enemyiesList.Count == 0)
                OnLastKilled();

        DisplayLevelProgress();
    }

    // Когда последний враг убит
    public void OnLastKilled()
    {
        YandexGame.savesData.NumberOfWaves++;
        YandexGame.savesData.Chapter++;
        YandexGame.savesData.NumberOfEnvironment++;
        if (YandexGame.savesData.NumberOfEnvironment > 2) YandexGame.savesData.NumberOfEnvironment = 0;
        YandexGame.SaveProgress();
        _winSound01.Play();
        _gameStateManager.SetWin();
        _gameSound.Pause();
    }

    public Enemy[] GetNearest(Vector3 point, int number)
    {
        // LINQ
        _enemyiesList = _enemyiesList.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();

        int returnNumber = Mathf.Min(number, _enemyiesList.Count);
        Enemy[] enemies = new Enemy[returnNumber];
        for (int i = 0; i < returnNumber; i++)
        {
            enemies[i] = _enemyiesList[i];
        }
        return enemies;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_playerTransform)
        {
            Handles.color = Color.white;
            Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _minRadius);
            Handles.color = Color.red;
            Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _maxRadius);
        }
    }
#endif

}
