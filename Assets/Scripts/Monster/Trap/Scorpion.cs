using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Monster {
    AnimationEvent animationEvent;

    private void Start() {
        base.SetForceKnockBack();
        animator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other) {
        if (!isAnimationRunning && other.CompareTag(Definition.TAG_PLAYER)) {
            Attack();
        }
    }

    public override void Attack() {
        base.Attack();
        particle.Play();
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;
        if (isAttackColliderOn) {
            SoundManager.init.PlayMonsterSound(Definition.SoundType.Enemy_Scorpion);
        }
        childAttackCollider.gameObject.SetActive(isAttackColliderOn);
    }

    private void DestroyObject() {
        Destroy(this.transform.parent.gameObject);
    }
}