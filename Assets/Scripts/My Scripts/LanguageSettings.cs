using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class LanguageSettings : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLang();

    public string CurrentLanguage; //RU EN
    [SerializeField] TextMeshProUGUI _languageText;
    public static LanguageSettings Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //CurrentLanguage = GetLang();
            _languageText.text = CurrentLanguage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Ru()
    {
        CurrentLanguage = "ru";
    }
    public void En()
    {
        CurrentLanguage = "en";
    }
}
