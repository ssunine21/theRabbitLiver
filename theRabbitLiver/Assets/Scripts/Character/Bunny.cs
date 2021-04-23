using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Character {
    [Range(0, 20)]
    public float speed;

    private int distance;

    private new void Start() {
        base.Start();
        distance = MOVE_OFFSET;
    }

    public override void Skill() {
        if (!isSkill) {
            targetPos = transform.position;
            targetPos.z -= distance;

            player.isStop = true;
            isSkill = true;
        }
    }

    private void FixedUpdate() {
        if (isSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if (transform.position.z <= (targetPos.z + targetPosRange)) {
                transform.position = targetPos;
                player.isStop = false;
                isSkill = false;
            }
        }
    }
}