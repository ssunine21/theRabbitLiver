using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter {
    public string SetInfoMessage();
    public int SkillLevel();
    public int LevelPrice();
    public int PurchasePrice();
    public void LevelUp();
    public DeviceData.CharacterID CharacterType();
}