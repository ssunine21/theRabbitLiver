using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish : Monster {
    AnimationEvent animationEvent;

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

    private Vector3 xPos;
    private readonly int XMAX = 9;
    private readonly int XMIN = -3;
    private bool isAttacking = false;

    private void Start() {
        base.SetForceKnockBack();
        animator = this.GetComponent<Animator>();

        StartCoroutine(nameof(SkillCoroutine));

        xPos = transform.position;
        xPos.x = Random.Range(0, 1) == 0 ? XMIN : XMAX;
    }

    private void FixedUpdate() {
        if (isAttacking) return;

        if (transform.position.x < 0) {
            xPos.x = XMAX;
        } else if (transform.position.x > Definition.TILE_SPACING * 2) {
            xPos.x = XMIN;
        }

        Quaternion rotationDir = xPos.x < 0 ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(Vector3.left);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDir, RotationSpeed);
        this.transform.position = Vector3.Lerp(transform.position, xPos, MoveSpeed);
    }

    public void IsAttack(int isAttack) {
        isAttacking = isAttack == 1 ? true : false;
    }


    public override void Attack() {
        if (!isAnimationRunning) {
            base.Attack();
        }
    }

    private void AttackColliderOnOff() {
        isAttackColliderOn = !isAttackColliderOn;
        childAttackCollider.gameObject.SetActive(isAttackColliderOn);
    }

    IEnumerator SkillCoroutine() {
        yield return new WaitForSeconds(Random.Range(0, attackDelay));

        while (true) {
            Attack();
            yield return new WaitForSeconds(attackDelay);
        }
    }

}