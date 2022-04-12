using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anglerfish : Monster {
    private readonly float INIT_ROTATE = 135f;

    public GameObject lightSphere;

    private void Start() {
        base.SetForceKnockBack();
        animator = this.GetComponent<Animator>();

        this.transform.localRotation = Quaternion.Euler(0, Time.deltaTime % 2 == 0 ? INIT_ROTATE : -INIT_ROTATE, 0);
    }

    private void OnTriggerStay(Collider other) {
        if (!isAnimationRunning && other.CompareTag(Definition.TAG_PLAYER)) {
            childAttackCollider.gameObject.SetActive(true);
            Attack();
        }
    }

    public override void Attack() {
        base.Attack();
        Destroy(lightSphere);
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;
        childAttackCollider.gameObject.SetActive(isAttackColliderOn);
    }

    private void DestroyObject() {
        Destroy(this.transform.parent.gameObject);
    }
}