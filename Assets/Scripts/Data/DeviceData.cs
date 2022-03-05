using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class DeviceData {
    public enum CharacterID {
        bunny, skeleton, bono, notake
    }

    public enum ItemID {
        coinplus, heartplus, protectionplus, protection
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