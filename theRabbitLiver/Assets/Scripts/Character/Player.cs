using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private float TRAP_DAMAGE = 0.1f;
	private float ENEMY_RECOVERY = 0.1f;

	public bool isStopping;
	public bool isSuperCharge;
	[Range(0, 20)]
	public float trapCrashSpeed;

	public HPBar hpBar;

	private bool isCrashing;
	private float crashErrorRange = 0.05f;

	private Vector3 crashPos;

    private void Update() {
		if (!isStopping) {
			if (Input.GetKeyDown(KeyCode.LeftArrow)
				|| Input.GetKeyDown(KeyCode.RightArrow)) {
				Move();
			}
		}
	}

	private void FixedUpdate() {
		if (isCrashing) {
			isStopping = true;

			transform.Translate((crashPos - transform.position) * trapCrashSpeed * Time.deltaTime);

			if (transform.position.z <= (crashPos.z + crashErrorRange)) {
				transform.position = crashPos;
				isStopping = false;
				isCrashing = false;
			}
		}
	}

	private void Move() {
		Vector3 offset = new Vector3();
		offset.z = this.transform.position.z + Definition.TILE_SPACING;
		RecordData.jumpCount++;

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (this.transform.position.x == 0)
				offset.x = Definition.TILE_SPACING * 2;
			else
				offset.x = this.transform.position.x - Definition.TILE_SPACING;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (this.transform.position.x == Definition.TILE_SPACING * 2)
				offset.x = 0;
			else
				offset.x = this.transform.position.x + Definition.TILE_SPACING;
		}

		this.transform.position = offset;
		//SpawnManager.init.SpawnTile();

		if (RecordData.jumpCount > 50)
			SpawnManager.init.RemoveTile();
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag(Definition.TAG_ENEMY)) {
			CollistionWithEnemy(collision);
		} else if (collision.gameObject.CompareTag(Definition.TAG_TRAP)) {
			CollistionWithTrap(collision);
		}
	}

	private void CollistionWithEnemy(Collision collision) {
		hpBar.hpBar += ENEMY_RECOVERY;
		Destroy(collision.gameObject);
	}

	private void CollistionWithTrap(Collision collision) {
		if (!isSuperCharge) {

			isCrashing = true;
			crashPos = transform.position;
			crashPos.z -= Definition.TILE_SPACING;
			hpBar.hpBar -= TRAP_DAMAGE;
		}

		Destroy(collision.gameObject);
	}
}