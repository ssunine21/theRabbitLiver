using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CharacterPurchase : MonoBehaviour {
    private readonly int MAX_LEVEL = 5;

    public GameObject[] characters;
    public GameObject levelBars;
    public GameObject unlockImg;
    public Camera characterViewCamera;

    public Vector3 pos;
    [Space(30)]
    public Button selectBtn;
    public Button levelUpBtn;

    private Dictionary<DeviceData.CharacterID, CloudData.CharacterProductInfo> characterProductInfoList;
    private int preIndex = 0;
    private int _index = 0;
    public DeviceData.CharacterID index {
        get { return  (DeviceData.CharacterID)_index; }
        set {
            int temp = (int) value;
            if (temp < 0) temp =  characters.Length - 1;
            else if (temp >= characters.Length) temp = 0;

            _index = temp;
        }
    }

    private void OnEnable() {
        foreach (var character in characters) {
            if (character == null) return;
            character.transform.localPosition = Vector3.one * 100;
        }
        characters[_index].transform.localPosition = pos;
        characterProductInfoList = DataManager.init.CloudData.characterProductInfoList;

        BtnNextCharacter();
    }

    public void BtnNextCharacter() {
        CharacterViewChange();
        CharacterInfoChange();
    }

    public void BtnNextCharacter(int _index) {
        preIndex = this._index;
        index += _index;

        CharacterViewChange();
        CharacterInfoChange();
    }

    private void CharacterViewChange() {
        characters[preIndex].transform.localPosition = Vector3.one * 100;
        characters[_index].transform.localPosition = pos;
    }

    private void CharacterInfoChange() {
        CloudData.CharacterProductInfo currCharInfo = characterProductInfoList[characters[_index].GetComponent<Character>()._Type];
        bool isPurchase = currCharInfo.isPurchase;

        BtnTextState(isPurchase);
        UnlockState(isPurchase);
        OnCharHide(!isPurchase);

        SetLevelBarColor(currCharInfo.skillLevel);
    }

    private void SetLevelBarColor(int level) {
        for(int i = 0; i < MAX_LEVEL; ++i) {
            Color color;
            if (i < level) color = Color.white;
            else color = new Color(1, 1, 1, 0.4f);

            levelBars.transform.GetChild(i).GetComponent<RawImage>().color = color;
        }
    }

    private void UnlockState(bool isPurchase) {
        unlockImg.SetActive(!isPurchase);
        levelUpBtn.interactable = isPurchase;
        levelUpBtn.GetComponent<Image>().color = isPurchase ? Color.white : new Color(1, 1, 1, 0.5f);
    }

    private void BtnTextState(bool isPurchase) {
        selectBtn.GetComponentInChildren<TextMeshProUGUI>().text = isPurchase ? Definition.SELECT : Definition.BUY;
        if (isPurchase && DataManager.init.DeviceData.characterId != (DeviceData.CharacterID)index) {
            selectBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
            selectBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
        } else {
            selectBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            selectBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void BtnLevelUp() {
        if (characterProductInfoList[index].isPurchase) {
            if (characterProductInfoList[index].skillLevel >= levelBars.transform.childCount) {
                UIManager.init.ShowAlert(Definition.BUY_ENOUGH, BtnNextCharacter); 
            } else {
                UIManager.init.ShowAlert(Definition.BUY_LEVEL_MASSAGE, characterProductInfoList[index].levelPrice, LevelUp, BtnNextCharacter);
            }
            OnCharHide(true);
        }
    }

    public void BtnSelectChar() {

        if (selectBtn.GetComponentInChildren<TextMeshProUGUI>().text.Equals(Definition.BUY)) {
            UIManager.init.ShowAlert(Definition.BUY_MASSAGE, characterProductInfoList[index].price, BuyChar, BtnNextCharacter);
        } else {    
            DataManager.init.DeviceData.characterId = (DeviceData.CharacterID)index;
            BtnTextState(true);
        }
    }

    private void LevelUp() {
        var obj = characterProductInfoList[index];
        if (DataManager.init.CoinComparison(obj.levelPrice, true)) {
            SetLevelBarColor(++obj.skillLevel);
            BtnNextCharacter();
        } else {
            UIManager.init.ShowAlert(Definition.NOT_ENOUGH_MONEY, BtnNextCharacter);
        }
    }

    private void BuyChar() {
        var obj = characterProductInfoList[index];

        if(obj.isPurchase) {
            UIManager.init.ShowAlert(Definition.BUY_ENOUGH, BtnNextCharacter);
            return;
        }

        if (DataManager.init.CoinComparison(obj.price, true)) {
            SetLevelBarColor(++obj.skillLevel);
            obj.isPurchase = true;
            BtnNextCharacter();
        } else {
            UIManager.init.ShowAlert(Definition.NOT_ENOUGH_MONEY, BtnNextCharacter);
        }
    }

    private void OnCharHide(bool isHide) {
        characterViewCamera.depth = isHide ? Definition.CAMERA_DEPTH_UNDER : Definition.CAMERA_DEPTH_OVER;
    }
}