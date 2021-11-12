using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    private readonly int hashAttack = Animator.StringToHash("attack");

    public static GameObject player;

    public Animator animator;

    private void Start() {
        animator = this.GetComponent<Animator>();
    }

    public virtual void Attack() {
        animator.SetTrigger(hashAttack);
    }
}