using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterPurchase : MonoBehaviour {

    public GameObject[] characters;
    public Vector3 pos;
    [Space(30)]
    public GameObject levelUpBtn;
    public TextMeshProUGUI levelInfoText;

    private ArrayList characterProductInfoList;
    private int preIndex = 0;
    private int _index = 0;
    public int index {
        get { return _index; }
        set {
            if (value < 0) value = characters.Length - 1;
            else if (value >= characters.Length) value = 0;

            _index = value;
        }
    }

    private void OnEnable() {
        foreach (var character in characters) {
            if (character == null) return;
            character.transform.localPosition = Vector3.one * 100;
        }
        characters[index].transform.localPosition = pos;
        characterProductInfoList = DataManager.init.CloudData.characterProductInfoList;
    }

    public void ButtnClick(bool isRight) {
        preIndex = index;
        index += isRight ? 1 : -1;

        CharacterViewChange();
        CharacterInfoChange();
    }

    private void CharacterViewChange() {
        characters[preIndex].transform.localPosition = Vector3.one * 100;
        characters[index].transform.localPosition = pos;
    }

    private void CharacterInfoChange() {
        foreach(var charProductInfo in characterProductInfoList) {
            //if(characters[index].name == (CloudData.CharacterProductInfo)charProductInfo.Name)
        }
    }
}