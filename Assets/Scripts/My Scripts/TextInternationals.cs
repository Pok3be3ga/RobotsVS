using TMPro;
using UnityEngine;

public class TextInternationals : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;
    void Start()
    {
        if (LanguageSettings.Instance.CurrentLanguage == "en")
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
        else if (LanguageSettings.Instance.CurrentLanguage == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = _ru;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = _ru;
        }
    }

}
