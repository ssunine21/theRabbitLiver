using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Monster {
    public GameObject attackCollider;


    AnimationEvent animationEvent;
    
    bool isAttackColliderOn = false;
    bool isAnimationRunning = false;

    private void Update() {
        if(player.transform.position.z < this.transform.position.z
            && player.transform.position.z >= this.transform.position.z - Definition.TILE_SPACING - 1
            && !isAnimationRunning) {
            Attack();
            isAnimationRunning = true;
            StartCoroutine(AttackDelay());
        }
    }

    public override void Attack() {
        base.Attack();
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;
        attackCollider.SetActive(isAttackColliderOn);
    }

    IEnumerator AttackDelay() {
        yield return new WaitForSeconds(5);
        isAnimationRunning = false;
    }
}