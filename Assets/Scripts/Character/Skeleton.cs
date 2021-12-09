using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Character {
    [Range(0, 20)]
    public float speed;
    private int distance;

    protected override void Start() {
        base.Start();
        distance = MOVE_OFFSET;
    }

    protected override void Ready() {

    }

    public override bool Skill() {
        if (!base.Skill()) return false;

        targetPos = transform.position;
        targetPos.z -= distance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if (transform.position.z <= (targetPos.z + posErrorRange)) {
                transform.position = targetPos;
                player.isGroggy = false;
                isUsingSkill = false;
            }
        }
    }
}