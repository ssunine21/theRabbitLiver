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
    public TextMeshProUGUI characterContents;
    public TextMeshProUGUI characterInfos;

    public Vector3 pos;
    [Space(30)]
    public Button selectBtn;
    public Button levelUpBtn;

    private ICharacter iCharacter;
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
        BtnNextCharacter();
    }

    public void BtnNextCharacter() {
        CharacterViewChange();
        CharacterInfoChange();
    }

    public void BtnNextCharacter(int _index) {
        preIndex = this._index;
        index += _index;
        SoundManager.init.PlaySFXSound(Definition.SoundType.ButtonClick);
        CharacterViewChange();
        CharacterInfoChange();
    }

    private void CharacterViewChange() {
        characters[preIndex].transform.localPosition = Vector3.one * 100;
        characters[_index].transform.localPosition = pos;
    }


    private void CharacterInfoChange() {
        iCharacter = characters[_index].GetComponent<ICharacter>();
        iCharacter.SetSkillLevel(DataManager.init.CloudData.characterLevel[iCharacter.CharacterType()]);
        bool isPurchase = iCharacter.SkillLevel() > 0 ? true : false;

        BtnTextState(isPurchase);
        UnlockState(isPurchase);
        OnCharHide(!isPurchase);

        SetLevelBarColor(iCharacter.SkillLevel());
        SetCharacterContents();
        SetCharacterInfos();
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
        characters[_index].transform.Find("model").GetComponent<SkinnedMeshRenderer>().material.color = isPurchase ? Color.white : Color.black;
    }

    private void BtnTextState(bool isPurchase) {

        selectBtn.GetComponentInChildren<TextMeshProUGUI>().text = isPurchase ?
            LocalizationManager.init.GetLocalizedValue("selected") :
            LocalizationManager.init.GetLocalizedValue("buy");
        if (isPurchase && DataManager.init.DeviceData.characterId != (DeviceData.CharacterID)index) {
            selectBtn.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.init.GetLocalizedValue("select");
            selectBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            selectBtn.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.2f);
        } else {
            selectBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            selectBtn.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
        }
    }

    public void BtnLevelUp() {
        SoundManager.init.PlaySFXSound(Definition.SoundType.ButtonClick);

        if (iCharacter.SkillLevel() > 0) {
            if ((iCharacter.SkillLevel() >= levelBars.transform.childCount)) {
                UIManager.init.ShowAlert(LocalizationManager.init.GetLocalizedValue("noMoreBuy"), BtnNextCharacter);
            } else {
                UIManager.init.ShowAlert(LocalizationManager.init.GetLocalizedValue("isLevelUp"), iCharacter.LevelPrice(), LevelUp, BtnNextCharacter);
            }
            OnCharHide(true);
        }
    }

    public void BtnSelectChar() {

        SoundManager.init.PlaySFXSound(Definition.SoundType.ButtonClick);
        if (selectBtn.GetComponentInChildren<TextMeshProUGUI>().text.Equals(LocalizationManager.init.GetLocalizedValue("buy"))) {
            UIManager.init.ShowAlert(LocalizationManager.init.GetLocalizedValue("isBuy"), iCharacter.PurchasePrice(), BuyChar, BtnNextCharacter);
        } else {    
            DataManager.init.DeviceData.characterId = (DeviceData.CharacterID)index;

            PlayerPrefs.SetInt("CharacterID", (int)DataManager.init.DeviceData.characterId);
            BtnTextState(true);
        }
    }

    private void LevelUp() {
        SoundManager.init.PlaySFXSound(Definition.SoundType.SFX_BUY);
        if (DataManager.init.CoinComparison(iCharacter.LevelPrice(), true)) {
            LevelUpAndAysnc();
            SetLevelBarColor(iCharacter.SkillLevel());
            BtnNextCharacter();
            SetCharacterInfos();

        } else {
            UIManager.init.ShowAlert(LocalizationManager.init.GetLocalizedValue("notEnoughMoney"), BtnNextCharacter);
        }
    }

    private void BuyChar() {
        if(iCharacter.SkillLevel() > 0) {
            UIManager.init.ShowAlert(LocalizationManager.init.GetLocalizedValue("noMoreBuy"), BtnNextCharacter);
            return;
        }

        if (DataManager.init.CoinComparison(iCharacter.PurchasePrice(), true)) {
            SoundManager.init.PlaySFXSound(Definition.SoundType.SFX_BUY);
            LevelUpAndAysnc();
            SetLevelBarColor(iCharacter.SkillLevel());
            BtnNextCharacter();
        } else {
            UIManager.init.ShowAlert(LocalizationManager.init.GetLocalizedValue("notEnoughMoney"), BtnNextCharacter);
        }
    }

    private void OnCharHide(bool isHide) {
        characterViewCamera.depth = isHide ? Definition.CAMERA_DEPTH_UNDER : Definition.CAMERA_DEPTH_OVER;
    }

    private void SetCharacterContents() {
        ICharacter iCharacter = characters[(int)index].GetComponent<ICharacter>();
        characterContents.text = iCharacter.GetContentMessage();
    }

    private void SetCharacterInfos() {
        ICharacter iCharacter = characters[(int)index].GetComponent<ICharacter>();
        characterInfos.text = iCharacter.GetInfoMessage();
    }

    private void LevelUpAndAysnc() {
        DataManager.init.CloudData.LevelUpAndAysnc(iCharacter.CharacterType());
        iCharacter.SetSkillLevel(DataManager.init.CloudData.characterLevel[iCharacter.CharacterType()]);
    }
}