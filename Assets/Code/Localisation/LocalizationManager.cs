using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    public TextAsset libRU;
    public TextAsset libEN;
    public Dictionary<string, string> localizedText;
    private bool isReady;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);




        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            LoadLocalizatedText(libRU);
            //PlayerPrefs.SetString("Language", "Russian");
        }
        else
        {
            LoadLocalizatedText(libEN);
            //PlayerPrefs.SetString("Language", "English");
        }
        if (PlayerPrefs.GetString("Language") == "Russian") LoadLocalizatedText(libRU);
        else LoadLocalizatedText(libEN);
            }



    public string GetLocalizetedValue(string key)
    {
        string result;
        result = "Error";
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public void LoadLocalizatedText(TextAsset lib)
    {
        localizedText = new Dictionary<string, string>();
        string dataAsJson = lib.ToString();
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        for (int i = 0; i<loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
        isReady = true;

    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
