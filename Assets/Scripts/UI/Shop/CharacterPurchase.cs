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

        BtnNextCharacter();
    }

    public void BtnNextCharacter() {
        CharacterViewChange();
        CharacterInfoChange();
    }

    public void BtnNextCharacter(int _index) {
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
            if(characters[index].GetComponent<Character>()._Type == charProductInfo.Name) {
                BtnTextState(charProductInfo.IsPurchase);
                UnlockState(charProductInfo.IsPurchase);
                SetLevelBarColor(charProductInfo.SkillLevel);
                return;
            }
        }
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
        characterViewCamera.depth = isPurchase ? Definition.CAMERA_DEPTH_OVER : Definition.CAMERA_DEPTH_UNDER;
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
        if (characterProductInfoList[index].IsPurchase) {
            UIManager.init.ShowAlert(Definition.BUY_LEVEL_MASSAGE, BuyLevel, BtnNextCharacter);
        }
    }

    public void BtnSelectChar() {
        if (selectBtn.GetComponentInChildren<TextMeshProUGUI>().text.Equals(Definition.BUY)) {
            UIManager.init.ShowAlert(Definition.BUY_CHARACATER_MASSAGE, BuyChar, BtnNextCharacter);
        } else {    
            DataManager.init.DeviceData.characterId = (DeviceData.CharacterID)index;
            BtnTextState(true);
        }
    }

    private void BuyLevel() {
        var obj = characterProductInfoList[index];

        if (CoinComparison(1000)) {
            CoinPayment(1000);
            SetLevelBarColor(++obj.SkillLevel);
            BtnNextCharacter();
        }
    }

    private void BuyChar() {
        var obj = characterProductInfoList[index];

        if (CoinComparison(obj.Price)) {
            CoinPayment(obj.Price);
            SetLevelBarColor(++obj.SkillLevel);
            obj.IsPurchase = true;
            BtnNextCharacter(0);
        }
    }

    private bool CoinComparison(int coin) {
        if(DataManager.init.CloudData.coin >= coin) {
            return true;
        } else {
            UIManager.init.ShowAlert(Definition.NOT_ENOUGH_MONEY, BtnNextCharacter);
            return false;
        }
    }

    private void CoinPayment(int price) {
        DataManager.init.CloudData.coin -= price;
    }
}