using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    protected static readonly int MOVE_OFFSET = Definition.TILE_SPACING;

    protected Player player;
    protected Vector3 targetPos;

    [SerializeField]
    protected UserData.CharacterID Type;
    [SerializeField]
    protected int level;

    protected float posErrorRange = 0.05f;
    protected bool isUsingSkill;

    virtual public bool Skill() {
        if (isUsingSkill || player.stamina.mpBar < 1) return false;

        player.isSuperCharge = true;
        player.isGroggy = true;
        isUsingSkill = true;
        return true;
    }

    virtual protected void Start() {
        player = GetComponent<Player>();
        player.character = this;
    }

    protected abstract void Ready();
}