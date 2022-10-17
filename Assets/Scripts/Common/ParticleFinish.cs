using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFinish : MonoBehaviour
{
    ParticleSystem particleSystem;

    private void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (!particleSystem.isPlaying) {
            Destroy(this.gameObject);
        }
    }
}
