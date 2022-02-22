using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Character {
    [Range(0, 20)]
    public float speed;
    private int distance = Definition.TILE_SPACING;

    protected override void Start() {
        base.Start();
        distance = MOVE_OFFSET;
    }

    protected override void Ready() { }

    public override bool Skill() {
        if (!base.Skill()) return false;
        player.isGroggy = true;

        targetPos = transform.position;
        targetPos.z -= distance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * speed);
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if (transform.position.z <= (targetPos.z + posErrorRange)) {
                transform.position = targetPos;

                player.isSuperCharge = false;
                player.isGroggy = false;
                isUsingSkill = false;
            }
        }
    }
}