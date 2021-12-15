using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬 : jump attack,
// 맞거나 죽을 때 : Shoulder Hit And Fall, Head Hit

public class Player : MonoBehaviour {
	private readonly int hashMoveRight = Animator.StringToHash("moveRight");
	private readonly int hashMoveLeft = Animator.StringToHash("moveLeft");
	private readonly int hashHitTheTrap = Animator.StringToHash("hitTheTrap");
	private readonly int hashDead = Animator.StringToHash("dead");
	private readonly int hashSkill = Animator.StringToHash("skill");

	private readonly float CHAR_DIRECTION = 0f;
	private readonly float TRAP_DAMAGE = 0.1f;
	private readonly float AMOUNT_RECOVERY_HP_ON_KILL = 0.1f;
	private readonly float AMOUNT_RECOVERY_MP_ON_KILL = 0.2f;

	[Range(0, 20)]
	public float trapCrashSpeed;

	public Stamina stamina;

	public bool isGroggy = false;
	public bool isSuperCharge = false;
	public bool isDead = false;

	private bool _hittingByTrap = false;
	public bool hittingByTrap {
		get { return _hittingByTrap; }
		set {
			_hittingByTrap = value;
			isGroggy = value;
		}
	}
	private float crashErrorRange = 0.05f;
	private Vector3 posAfterHit;

	private Animator animator;

	private Character _character;
	public Character character {
		get { return _character; }
		set { _character = value; }
	}

	private void Start() {
		animator = GetComponent<Animator>();
	}

	private void Update() {
		if (!isGroggy && !isDead) {
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				Move(true);
			} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
				Move(false);
			}
		}

		if (stamina.hpBar <= 0 && !isDead) {
			//animator.SetTrigger(hashDead);
			//isDead = true;
		}
	}

	private void FixedUpdate() {
		if (hittingByTrap) {
			this.transform.position = Vector3.Lerp(this.transform.position, posAfterHit, Time.deltaTime * trapCrashSpeed);

			if (transform.position.z <= (posAfterHit.z + crashErrorRange)) {
				transform.position = posAfterHit;
				if (this.transform.position.z % 3 != 0) {
					Vector3 vector3 = this.transform.position;
					vector3.z -= (this.transform.transform.position.z % 3);
				}

				hittingByTrap = false;
				isSuperCharge = false;
			}
		}
	}

	private void Move(bool isLeft) {
		Vector3 offset = new Vector3(0, this.transform.position.y, 0);
		Vector3 quaternion = new Vector3();

		offset.z = this.transform.position.z + Definition.TILE_SPACING;
		RecordData.jumpCount++;

		if (isLeft) {
			animator.SetTrigger(hashMoveLeft);

			quaternion.y = -CHAR_DIRECTION;
			if (this.transform.position.x == 0)
				offset.x = Definition.TILE_SPACING * 2;
			else
				offset.x = this.transform.position.x - Definition.TILE_SPACING;
		} else {
			animator.SetTrigger(hashMoveRight);

			quaternion.y = CHAR_DIRECTION;
			if (this.transform.position.x == Definition.TILE_SPACING * 2)
				offset.x = 0;
			else
				offset.x = this.transform.position.x + Definition.TILE_SPACING;
		}

		this.transform.position = offset;
		this.transform.rotation = Quaternion.Euler(quaternion);

		DataManager.init.score.currScore += 50;
	}

	private void OnTriggerEnter(Collider collision) {
		Debug.Log("OnTriggerEnter - " + collision);

		if (collision.gameObject.CompareTag(Definition.TAG_ENEMY)) {
			CollisionWithEnemy(collision);
		} else if (collision.gameObject.CompareTag(Definition.TAG_ATTACK_COLLIDER)) {
			CollisionWithAttack(collision);
		}
	}

	private void CollisionWithEnemy(Collider collision) {
		stamina.hpBar += AMOUNT_RECOVERY_HP_ON_KILL;
		stamina.mpBar += AMOUNT_RECOVERY_MP_ON_KILL;

		Destroy(collision.gameObject);
	}

	private void CollisionWithAttack(Collider collider) {
		if (!isSuperCharge) {
			Hitting(collider);
			//stamina.hpBar -= 30;
		}
	}

	private void Hitting(Collider collider) {
		posAfterHit = this.transform.position;
		try {
			posAfterHit.z -= (Definition.TILE_SPACING * collider.GetComponent<AttackColliderInfo>().force_KnockBack);
		} catch (Exception e) {
			Debug.LogError(e.StackTrace);
        }
		animator.SetTrigger(hashHitTheTrap);

		hittingByTrap = true;
		isSuperCharge = true;
	}

	public void Skill() {
		if(character.Skill()) {
			animator.SetTrigger(hashSkill);
		}
	}
}