using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour {
    protected static readonly int MOVE_OFFSET = Definition.TILE_SPACING;

    protected Player player;
    protected Vector3 targetPos;

    protected int level;
    protected float targetPosRange = 0.05f;
    protected bool isSkill;

    abstract public void Skill();

    protected void Start() {
        player = GetComponent<Player>();
    }
}