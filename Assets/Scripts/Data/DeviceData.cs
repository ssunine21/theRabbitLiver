using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class DeviceData {

    private string deviceName;
    public int isPremium = 0;

    public string GetUID() {
        return PlayerPrefs.GetString(Definition.KEY_UID);
    }

    public enum CharacterID {
        bunny, skeleton, bono, jazz, notake, Max
    }

    public void Save() {
        PlayerPrefs.SetInt("SFXVolum", OptionButton.volumOption[0]);
        PlayerPrefs.SetInt("BGMVolum", OptionButton.volumOption[1]);
        PlayerPrefs.SetInt("Viberation", OptionButton.volumOption[2]);
        PlayerPrefs.SetInt("CharacterID", (int)characterId);
        PlayerPrefs.SetInt("isPremium", isPremium);
    }

    public void Load() {
        characterId = (CharacterID)PlayerPrefs.GetInt("CharacterID");
        isPremium = PlayerPrefs.GetInt("isPremium");
    }

    public enum ItemID {
        coinPlus, heartPlus, protectionPlus, heart, protection, skip, Max
    }

    private CharacterID _characterId = CharacterID.bunny;
    public CharacterID characterId {
        get {
            return _characterId;
        }
        set {
            if (value < Enum.GetValues(typeof(CharacterID)).Cast<CharacterID>().First()
                || value > Enum.GetValues(typeof(CharacterID)).Cast<CharacterID>().Last())
                _characterId = CharacterID.bunny;
            else
                _characterId = value;
        }
    }
}