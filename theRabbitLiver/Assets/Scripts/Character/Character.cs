using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    protected static readonly int MOVE_OFFSET = Definition.TILE_SPACING;

    protected Player player;
    protected Vector3 targetPos;

    [SerializeField]
    protected int level;
    protected float posErrorRange = 0.05f;
    protected bool isUsingSkill;

    virtual public bool Skill() {
        if (isUsingSkill) return false;

        player.isSuperCharge = true;
        player.isStopping = true;
        isUsingSkill = true;
        return true;
    }

    virtual protected void Start() {
        player = GetComponent<Player>();
    }
}