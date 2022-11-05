using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anglerfish : Monster {
    public GameObject lightSphere;

    //225 160
    private void Start() {
        base.SetForceKnockBack();
        animator = this.GetComponent<Animator>();
        Transform parent = this.transform.GetComponentInParent<Transform>().parent;

        parent.rotation = Random.Range(0, 2) == 0 ? Quaternion.Euler(0, 155, 0) : Quaternion.Euler(0, 205, 0);
    }

    private void OnTriggerStay(Collider other) {
        if (!isAnimationRunning && other.CompareTag(Definition.TAG_PLAYER)) {
            childAttackCollider.gameObject.SetActive(true);
            Attack();

            if(particle != null) {
                particle.Play();
            }
        }
    }

    public override void Attack() {
        base.Attack();
        Destroy(lightSphere);
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;
        if (isAttackColliderOn) {
            SoundManager.init.PlayMonsterSound(Definition.SoundType.Enemy_Anglerfish);
        }
        childAttackCollider.gameObject.SetActive(isAttackColliderOn);
    }

    private void DestroyObject() {
        Destroy(this.transform.parent.gameObject);
    }
}