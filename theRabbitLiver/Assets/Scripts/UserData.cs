using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class UserData {
    public enum CharacterID {
        bunny, bono, notake
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