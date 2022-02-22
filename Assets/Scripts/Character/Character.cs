using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    protected static readonly int MOVE_OFFSET = Definition.TILE_SPACING;

    protected Player player;
    protected Vector3 targetPos;

    [SerializeField]
    protected DeviceData.CharacterID Type;
    public DeviceData.CharacterID _Type { get { return Type; } }
    [SerializeField]
    protected int level;

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
    }

    protected abstract void Ready();
}