
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class ProgressData
{
    [SerializeField] private int _healthLevel;
    [SerializeField] private int _damageLevel;
    [SerializeField] private int _lootLevel;
    [SerializeField] private int _chapter;
    [SerializeField] private float _coins;
    [SerializeField] private int _numberOfWaves = 5;



    public int NumberOfWaves
    {
        get => _numberOfWaves; set
        {
            _numberOfWaves = value;
            SaveSystem.Save(this);
        }
    }
    public int HealthLevel
    {
        get => _healthLevel; set
        {
            _healthLevel = value;
            SaveSystem.Save(this);
        }
    }
    public int DamageLevel
    {
        get => _damageLevel; set
        {
            _damageLevel = value;
            SaveSystem.Save(this);
        }
    }
    public int LootLevel
    {
        get => _lootLevel; set
        {
            _lootLevel = value;
            SaveSystem.Save(this);
        }
    }
    public int Chapter
    {
        get => _chapter; set
        {
            _chapter = value;
            SaveSystem.Save(this);
        }
    }
    public float Coins
    {
        get => _coins; set
        {
            _coins = value;
            SaveSystem.Save(this);
        }
    }
}


public class Progress : MonoBehaviour
{
    public ProgressData ProgressData;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();




    public static Progress InstanceProgress;

    public int IndexRobot = 0;
    public int IndexChapter = 0;

    public void Init()
    {
        ProgressData = SaveSystem.Load();
    }

    private void Awake()
    {
        if (InstanceProgress == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            InstanceProgress = this;
#if UNITY_WEBGL
            LoadExtern();
#endif
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void SetProgressData(string value)
    {
        ProgressData = JsonUtility.FromJson<ProgressData>(value);
    }
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(ProgressData);
        SaveExtern(jsonString);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Progress))]
public class ProgressEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Progress progress = target as Progress;

        if (GUILayout.Button("SAVE"))
        {
            SaveSystem.Save(progress.ProgressData);
        }

        if (GUILayout.Button("LOAD"))
        {
            progress.ProgressData = SaveSystem.Load();
        }
        if (GUILayout.Button("RESET PROGRESS"))
        {
            progress.ProgressData = new ProgressData();
            SaveSystem.Save(progress.ProgressData);
        }
    }
}
#endif
