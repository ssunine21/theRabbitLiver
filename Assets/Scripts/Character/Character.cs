using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
  
    protected static readonly int MOVE_OFFSET = Definition.TILE_SPACING;
    public Vector3 startPosOffset = Vector3.zero;

    private Player _player;
    protected Player player {
        get => _player = _player == null ? GetComponent<Player>() : _player;
    }

    protected Vector3 targetPos;

    [SerializeField]
    protected DeviceData.CharacterID Type;
    public DeviceData.CharacterID _Type { get { return Type; } }
    protected int[] levelPrice;
    protected int purchasePrice;

    public int level;

    protected float posErrorRange = 0.05f;
    protected bool isUsingSkill = false;

    public float hpDecreasingSpeed;
    public float mpIncreasing;

    virtual public bool Skill() {
        if (isUsingSkill || player.isGroggy) return false;

        player.isSuperCharge = true;
        isUsingSkill = true;
        return true;
    }
    virtual public void GameSetting() {
        this.transform.position = new Vector3(3, 0, 3);
        this.transform.position += startPosOffset;
        level = DataManager.init.CloudData.characterLevel[_Type];
    }

    protected virtual void Start() {

    }
}