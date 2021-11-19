using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Monster {
    AnimationEvent animationEvent;

    private void Start() {
        base.SetForceKnockBack();
        animator = this.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other) {
        if (!isAnimationRunning && other.CompareTag("Player")) {
            Attack();
        }
    }

    public override void Attack() {
        base.Attack();
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;
        childAttackCollider.gameObject.SetActive(isAttackColliderOn);
    }

    private void DestroyObject() {
        Destroy(this.transform.parent.gameObject);
    }
}