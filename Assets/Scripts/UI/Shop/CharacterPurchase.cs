using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        BtnNextCharacter(0);
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
    }

    public void BtnLevelUp() {
        if (characterProductInfoList[index].IsPurchase) {
            UIManager.init.ShowAlert(Definition.BUY_LEVEL_MASSAGE, CheckBtnFun);
        }
    }

    public void BtnSelectChar() {
        if (selectBtn.GetComponentInChildren<TextMeshProUGUI>().text.Equals(Definition.BUY)) {
            UIManager.init.ShowAlert(Definition.BUY_CHARACATER_MASSAGE, CheckBtnFun);
        } else {    
            DataManager.init.DeviceData.characterId = (DeviceData.CharacterID)index;
        }
    }

    private void CheckBtnFun() {
        if(DataManager.init.CloudData.coin >= 1000) {
            DataManager.init.CloudData.coin -= 1000;
            BuyChar(index);
            SetLevelBarColor(++characterProductInfoList[index].SkillLevel);
            BtnNextCharacter(0);
        } else {
            UIManager.init.ShowAlert(Definition.NOT_ENOUGH_MONEY);
        }
    }

    private void BuyChar(int index) {
        characterProductInfoList[index].IsPurchase = true;
    }
}