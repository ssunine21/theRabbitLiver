using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	private static readonly int moveOffset = 3;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow) 
			|| Input.GetKeyDown(KeyCode.RightArrow))
			Move();
	}

	private void Move() {
		Vector3 offset = new Vector3();
		offset.z = this.transform.position.z + moveOffset;
		RecordData.jumpCount++;

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (this.transform.position.x == 0)
				offset.x = moveOffset * 2;
			else
				offset.x = this.transform.position.x - moveOffset;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (this.transform.position.x == moveOffset * 2)
				offset.x = 0;
			else
				offset.x = this.transform.position.x + moveOffset;
		}

		this.transform.position = offset;
		SpawnManager.init.SpawnTile();

		if (RecordData.jumpCount > 4)
			SpawnManager.init.RemoveTile();
	}
}