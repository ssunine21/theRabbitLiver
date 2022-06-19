using System;
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
    protected int[] levelPrice;
    protected int purchasePrice;

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
        try {
            player = GetComponent<Player>();
            player.character = this;
        } catch(NullReferenceException NRE) {
#if DEBUG
            //UnityEngine.Debug.LogError(NRE.Message);
#endif
        } finally {
            level = DataManager.init.CloudData.characterLevel[_Type];
        }
    }

    protected abstract void Ready();
}