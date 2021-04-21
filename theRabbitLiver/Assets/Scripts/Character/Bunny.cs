using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Character {
    private void Start() {
        player = this.GetComponent<Player>();
    }

    public override void Skill() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - MOVE_OFFSET);
    }
}