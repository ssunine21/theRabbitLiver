using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬 : jump attack,
// 맞거나 죽을 때 : Shoulder Hit And Fall, Head Hit

public class Player : MonoBehaviour {
	private readonly int hashMoveRight = Animator.StringToHash("moveRight");
	private readonly int hashMoveLeft = Animator.StringToHash("moveLeft");

	private readonly float CHAR_DIRECTION = 0f;
	private readonly float TRAP_DAMAGE = 0.1f;
	private readonly float AMOUNT_RECOVERY_HP_ON_HEALTH_ITEM = 0.3f;
	private readonly float AMOUNT_RECOVERY_HP_ON_KILL = 0.1f;
	private readonly float AMOUNT_RECOVERY_MP_ON_KILL = 0.2f;


	[Range(0, 20)]
	public float trapCrashSpeed;

	public Stamina stamina;

	public bool isStopping;
	public bool isSuperCharge;
	private bool _isCrashing;
	public bool isCrashing {
        get { return _isCrashing; }
		set {
			_isCrashing = value;
			isStopping = value;
		}
    }
	private float crashErrorRange = 0.05f;

	private Animator animator;
	private Vector3 crashPos;

	private Character _character;
	public Character character {
		get { return _character; }
        set { _character = value; }
    }

    private void Start() {
		animator = GetComponent<Animator>();
    }

    private void Update() {
		if (!isStopping) {
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
				Move();
			}
		}
	}

	private void FixedUpdate() {
		if (isCrashing) {

			// Translate를 사용하지 말고 바로 위치를 옮긴 후 애니메이션으로 보정해보자 =======
			// 애니메이션이 끝나면 isCrash를 false 로 ===============================
			//transform.Translate((crashPos - transform.position) * trapCrashSpeed * Time.deltaTime);

			//if (transform.position.z <= (crashPos.z + crashErrorRange)) {
			//	transform.position = crashPos;
			//	isCrashing = false;
			//}
		}
	}

	private void Move() {
		Vector3 offset = new Vector3();
		Vector3 quaternion = new Vector3();

		offset.z = this.transform.position.z + Definition.TILE_SPACING;
		RecordData.jumpCount++;

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			animator.SetTrigger(hashMoveLeft);

			quaternion.y = -CHAR_DIRECTION;
			if (this.transform.position.x == 0)
				offset.x = Definition.TILE_SPACING * 2;
			else
				offset.x = this.transform.position.x - Definition.TILE_SPACING;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			animator.SetTrigger(hashMoveRight);

			quaternion.y = CHAR_DIRECTION;
			if (this.transform.position.x == Definition.TILE_SPACING * 2)
				offset.x = 0;
			else
				offset.x = this.transform.position.x + Definition.TILE_SPACING;
		}

		this.transform.position = offset;
		this.transform.rotation = Quaternion.Euler(quaternion);

		if (RecordData.jumpCount > 50)
			SpawnManager.init.RemoveTile();
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag(Definition.TAG_ENEMY)) {
			CollisionWithEnemy(collision);
		} else if (collision.gameObject.CompareTag(Definition.TAG_TRAP)) {
			CollisionWithTrap(collision);
		} else if (collision.gameObject.CompareTag(Definition.TAG_HEALTH_ITEM)) {
			CollisionWithHealthItem(collision);

		}
	}

	private void CollisionWithEnemy(Collision collision) {
		stamina.hpBar += AMOUNT_RECOVERY_HP_ON_KILL;
		stamina.mpBar += AMOUNT_RECOVERY_MP_ON_KILL;

		Destroy(collision.gameObject);
	}

	private void CollisionWithTrap(Collision collision) {
		if (!isSuperCharge) {

			crashPos = transform.position;
			crashPos.z -= Definition.TILE_SPACING;

			transform.position = crashPos;
			stamina.hpBar -= TRAP_DAMAGE;
		}

		Destroy(collision.gameObject);
	}

	private void CollisionWithHealthItem(Collision collision) {
		stamina.hpBar += AMOUNT_RECOVERY_HP_ON_HEALTH_ITEM;
    }
}