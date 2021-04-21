using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private static readonly int MOVE_OFFSET = 3;

    private void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow) 
			|| Input.GetKeyDown(KeyCode.RightArrow))
			Move();
	}

	private void Move() {
		Vector3 offset = new Vector3();
		offset.z = this.transform.position.z + MOVE_OFFSET;
		RecordData.jumpCount++;

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (this.transform.position.x == 0)
				offset.x = MOVE_OFFSET * 2;
			else
				offset.x = this.transform.position.x - MOVE_OFFSET;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (this.transform.position.x == MOVE_OFFSET * 2)
				offset.x = 0;
			else
				offset.x = this.transform.position.x + MOVE_OFFSET;
		}

		this.transform.position = offset;
		//SpawnManager.init.SpawnTile();

		if (RecordData.jumpCount > 4)
			SpawnManager.init.RemoveTile();
	}
}