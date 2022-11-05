using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murloc : Monster {
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
        if (isAttackColliderOn) {
            SoundManager.init.PlayMonsterSound(Definition.SoundType.Enemy_Muloc);
        }
        childAttackCollider.gameObject.SetActive(isAttackColliderOn);
    }
}