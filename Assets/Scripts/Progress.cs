
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    public int HealthLevel;
    public int DamageLevel;
    public int LootLevel;
    public int Chapter;
    public float Coins;
    public int NumberOfWaves = 5;
    public int NumberOfEnvironment;

    public bool[] RobotBuy = { false, false, false };
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
    public void ResetProogress()
    {
        ProgressData = new ProgressData();
        SaveSystem.Save(ProgressData);
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
