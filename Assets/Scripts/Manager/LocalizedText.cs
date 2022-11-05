using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LocalizedText : MonoBehaviour {

    public string key;

	private void OnEnable() {
		TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
		text.text = LocalizationManager.init.GetLocalizedValue(key);
	}
}