using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    protected readonly int hashAttack = Animator.StringToHash("attack");

    protected bool isAttackColliderOn = false;
    protected bool isAnimationRunning = false;
    [SerializeField]protected ParticleSystem particle;

    public static GameObject player;

    public AttackColliderInfo childAttackCollider;
    public Animator animator;

    [Range(1, 5)] public int force_KnockBack;
    [Range(.0f, 1f)] public float damage;
    [Range(.0f, 10f)] public float attackDelay;

    public virtual void Attack() {
        try {
            animator.SetTrigger(hashAttack);
        } catch(MissingComponentException e) {
            animator = GetComponentInChildren<Animator>();
#if (DEBUG)
            UnityEngine.Debug.Log(e.Message);
#endif
        }
        isAnimationRunning = true;
        StartCoroutine(nameof(AttackDelay));
    }

    public virtual void SetForceKnockBack() {
        try {
            childAttackCollider.force_KnockBack = this.force_KnockBack;
            childAttackCollider.gameObject.SetActive(false);
        } catch(NullReferenceException e) {
            Debug.Log("childAttackCollider is null :: " + e);
        }
    }

    public virtual IEnumerator AttackDelay() {
        yield return new WaitForSeconds(attackDelay);
        isAnimationRunning = false;
    }
}