using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bono : Character {
    [Range(0, 20)]
    public float speed;
    private int skillMoveDistance;
    //BONO : Bono uses skill what move forward.

    protected override void Start() {
        base.Start();
    }

    protected override void Ready() {
    }

    public override bool Skill() {
        if (!base.Skill()) return false;
        skillMoveDistance = level * MOVE_OFFSET;

        player.isGroggy = true;

        targetPos = transform.position;
        targetPos.z += skillMoveDistance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if(transform.position.z >= (targetPos.z - posErrorRange)) {
                transform.position = targetPos;
                player.isGroggy = false;
                isUsingSkill = false;
            }
        }
    }
}