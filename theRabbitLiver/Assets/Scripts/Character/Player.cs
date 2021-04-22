using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool isStop;

    private void Update() {
		if (!isStop) {
			if (Input.GetKeyDown(KeyCode.LeftArrow)
				|| Input.GetKeyDown(KeyCode.RightArrow)) {
				Move();
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
}