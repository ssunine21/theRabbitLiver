using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class DeviceData {

    private string deviceName;

    public string GetUID() {
        return PlayerPrefs.GetString(Definition.KEY_UID);
    }

    public enum CharacterID {
        bunny, skeleton, bono, jazz, notake
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