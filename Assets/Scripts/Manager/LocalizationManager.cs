using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {
    public static LocalizationManager init;

    private string _lastLanguageFileName;
    public string lastLanguageFileName {
		get { return _lastLanguageFileName; }
		set {
            if (value.Equals("")) {
                InitLanguage();
            } else {
                _lastLanguageFileName = value;
                LoadLocalizedText(value);
            }
        }
	}

    private TextAsset textData;
    private Dictionary<string, string> localizedText;
    private string missingTextString = "Localized text not found";

    private void Awake() {
        if (init == null) {
            init = this;
        } else if (init != this) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        InitLanguage();
    }

    public void LoadLocalizedText(string fileName) {
        _lastLanguageFileName = fileName;

        //fileName += ".json";
        textData = Resources.Load<TextAsset>("localized/" + fileName);

        localizedText = new Dictionary<string, string>();

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(textData.ToString());

        for (int i = 0; i < loadedData.items.Length; ++i) {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        if(UIManager.init.OptionUIActiveSelf()) {
            UIManager.init.ToMainFromOptionBtn();
            UIManager.init.OptionBtn();
        }
    }

    public string GetLocalizedValue(string key) {
        string result = missingTextString;
        if (localizedText.ContainsKey(key)) {
            result = localizedText[key];
        }

        return result;
    }

    private void InitLanguage() {
        string languageName = "en";

		switch (Application.systemLanguage) {
            case SystemLanguage.Korean:
                languageName = "ko";
                break;
            case SystemLanguage.German:
                languageName = "de";
                break;
            case SystemLanguage.Spanish:
                languageName = "es";
                break;
            case SystemLanguage.French:
                languageName = "fr";
                break;
            case SystemLanguage.Italian:
                languageName = "it";
                break;
            case SystemLanguage.Japanese:
                languageName = "ja";
                break;
            case SystemLanguage.Portuguese:
                languageName = "pt";
                break;
            case SystemLanguage.ChineseSimplified:
                languageName = "zh_chs";
                break;
            default:
                languageName = "en";
                break;

        }

        languageName = "localizedText_" + languageName;
        LoadLocalizedText(languageName);
    }
}