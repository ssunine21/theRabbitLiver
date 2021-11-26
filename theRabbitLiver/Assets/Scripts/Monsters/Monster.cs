using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    private readonly int hashAttack = Animator.StringToHash("attack");

    protected bool isAttackColliderOn = false;
    protected bool isAnimationRunning = false;

    public static GameObject player;

    public AttackColliderInfo childAttackCollider;
    public Animator animator;

    public int force_KnockBack = 0;

    public virtual void Attack() {
        animator.SetTrigger(hashAttack);
        isAnimationRunning = true;
    }

    public virtual void SetForceKnockBack() {
        try {
            childAttackCollider.force_KnockBack = this.force_KnockBack;
            childAttackCollider.gameObject.SetActive(false);
        } catch(NullReferenceException e) {
            Debug.Log("childAttackCollider is null");
        }
    }

    public virtual IEnumerator AttackDelay(int delay) {
        yield return new WaitForSeconds(delay);
        isAnimationRunning = false;
    }
}