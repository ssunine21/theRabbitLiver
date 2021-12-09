using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coral : Monster {
    AnimationEvent animationEvent;

    public GameObject bullet;
    private GameObject[] bullets = new GameObject[3];

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
        StartCoroutine(AttackDelay(5));
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;

        for (int i = 0; i < bullets.Length; ++i) {
            bullets[i] = Instantiate(bullet, this.transform.position, this.transform.rotation, this.transform);
        }

        bullets[0].GetComponent<CoralBullet>().direction = Vector3.forward;
        bullets[1].GetComponent<CoralBullet>().direction = Vector3.left;
        bullets[2].GetComponent<CoralBullet>().direction = Vector3.right;
    }
}