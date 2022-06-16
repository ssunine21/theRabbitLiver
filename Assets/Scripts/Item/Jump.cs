using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Item, IItem {
    public void Use() {
        StartCoroutine(nameof(JumpCorountine));
    }

    IEnumerator JumpCorountine() {
        for(int i = 0; i < 20; ++i) {
            player.Move();
            yield return null;
        }
    }
}