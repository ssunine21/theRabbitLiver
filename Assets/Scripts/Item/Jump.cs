using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Item, IItem {

    int _tileCount;

    public void Use() {
        _tileCount = SpawnManager.init.levelDesign.tile.tileCount - GameManager.init.recordData.runCount;
        StartCoroutine(nameof(JumpCorountine));
    }

    IEnumerator JumpCorountine() {
        for(int i = 0; i < _tileCount; ++i) {
            player.Move();
            yield return null;
        }
    }
}