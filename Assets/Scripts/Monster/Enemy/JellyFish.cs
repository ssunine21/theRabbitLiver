using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish : Monster {
    AnimationEvent animationEvent;
    Transform tr;

    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed {
        get { return _rotationSpeed * Time.deltaTime; }
        set { _rotationSpeed = value; }
    }
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed {
        get { return _moveSpeed * Time.deltaTime; }
        set { _moveSpeed = value; }
    }

    public ParticleSystem attackParticle;
    public ParticleSystem preAttack;

    private Vector3 xPos;
    private readonly int XMAX = 9;
    private readonly int XMIN = -3;
    private bool isAttacking = false;

    private void Start() {
        base.SetForceKnockBack();
        animator = this.GetComponent<Animator>();
        tr = this.GetComponent<Transform>();

        xPos = tr.position;
        xPos.x = UnityEngine.Random.Range(0, 1) == 0 ? XMIN : XMAX;

        StartCoroutine(nameof(SkillCoroutine));
    }

    private void FixedUpdate() {
        if (isAttacking) return;

        if (tr.position.x < 0) {
            xPos.x = XMAX;
        } else if (tr.position.x > Definition.TILE_SPACING * 2) {
            xPos.x = XMIN;
        }

        Quaternion rotationDir = xPos.x < 0 ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(Vector3.left);
        tr.rotation = Quaternion.RotateTowards(tr.rotation, rotationDir, RotationSpeed);
        tr.position = Vector3.Lerp(tr.position, xPos, MoveSpeed);
    }

    public void IsAttack(int isAttack) {
        isAttacking = isAttack == 1 ? true : false;

        if (preAttack != null)
            preAttack.Play();
    }


    public override void Attack() {
        if (!isAnimationRunning) {
            base.Attack();
        }
    }

    private void AttackColliderOnOff() {
        try {
            isAttackColliderOn = !isAttackColliderOn;
            childAttackCollider.gameObject.SetActive(isAttackColliderOn);

            if (isAttackColliderOn) {
                if (attackParticle != null)
                    attackParticle.Play();
            } else {
                if (preAttack.isPlaying)
                    preAttack.Stop();
            }
        } catch (Exception e) { 
            Debug.LogError(e); 
        }
    }

    IEnumerator SkillCoroutine() {

        while (true) {
            if (tr.position.x < 0.1 || (tr.position.x >= 2.95 && tr.position.x <= 3.05) || tr.position.x >= 6) {
                int rand = UnityEngine.Random.Range(0, 10);
                if (rand % 2 == 0) {
                    Attack();
                    yield return new WaitForSeconds(attackDelay);
                }

                yield return new WaitForSeconds(.5f);
            }

            yield return null;
        }
    }

}