using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Monster {
    AnimationEvent animationEvent;

    private void Start() {
        base.SetForceKnockBack();
        animator = this.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other) {
        if (!isAnimationRunning && other.CompareTag(Definition.TAG_PLAYER)) {
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
}