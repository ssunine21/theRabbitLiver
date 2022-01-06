using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPurchase : MonoBehaviour {

    public GameObject[] characters;
    public Vector3 pos;
    [Space(30)]
    public Button levelUpBtn;
    public TextMeshProUGUI levelInfoText;

    private List<CloudData.CharacterProductInfo> characterProductInfoList;
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

        ButtnClick(0);
    }

    public void ButtnClick(int _index) {
        preIndex = index;
        index += _index;

        CharacterViewChange();
        CharacterInfoChange();
    }

    private void CharacterViewChange() {
        characters[preIndex].transform.localPosition = Vector3.one * 100;
        characters[index].transform.localPosition = pos;
    }

    private void CharacterInfoChange() {
        foreach(var charProductInfo in characterProductInfoList) {
            if(characters[index].name == charProductInfo.Name) {
                BtnTextState(charProductInfo.IsPurchase);
                levelInfoText.text = "Lv. " + charProductInfo.SkillLevel.ToString();

                return;
            }
        }
    }

    private void BtnTextState(bool isPurchase) {
        //TODO 번역 넣을 때 텍스트 변경
        levelUpBtn.GetComponentInChildren<TextMeshProUGUI>().text = isPurchase ? "레벨업" : "구매하기";
    }

    public void BtnSelectChar() {
        DataManager.init.DeviceData.characterId = (DeviceData.CharacterID)index;
    }
}