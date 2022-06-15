using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    public enum levelKey {
        healthIncrease
    }

    protected static readonly int MOVE_OFFSET = Definition.TILE_SPACING;

    protected Player player;
    protected Vector3 targetPos;

    [SerializeField]
    private float[] _hpIncrease;
    public float hpIncrease {
        get {
            level -= 1;
            if (level < 0) level = 0;
            else if (level > 4) level = 4;
            return _hpIncrease[level];
        }
    }

    public int skillUsageReductionCount;

    [SerializeField]
    protected DeviceData.CharacterID Type;
    public DeviceData.CharacterID _Type { get { return Type; } }

    public int level;

    protected float posErrorRange = 0.05f;
    protected bool isUsingSkill = false;

    virtual public bool Skill() {
        if (isUsingSkill || player.isGroggy) return false;

        player.isSuperCharge = true;
        isUsingSkill = true;
        return true;
    }

    virtual protected void Start() {
        player = GetComponent<Player>();
        player.character = this;

        skillUsageReductionCount = DataManager.init.CloudData.characterProductInfoList[_Type].hpincrease;
        level = DataManager.init.CloudData.characterProductInfoList[_Type].skillLevel;
    }

    protected abstract void Ready();
}