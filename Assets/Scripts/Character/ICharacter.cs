using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter {
    public string SetInfoMessage();
    public int SkillLevel();
    public int LevelPrice();
    public int PurchasePrice();
    public void LevelUp();
    public void GameSetting();
    public float hpDecreasing { get; }
    public float mpIncreasing { get; }
    public bool Skill();
    public int SkillCount();
    public DeviceData.CharacterID CharacterType();
}