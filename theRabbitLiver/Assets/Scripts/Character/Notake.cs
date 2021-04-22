using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notake : Character {
    [Range(0, 20)]
    public float skillDelay;
    [Range(0, 20)]
    public float skillRange;

    public LayerMask layerMask;

    private new void Start() {
        base.Start();
        level = 3;
    }

    public override void Skill() {
        if (!isSkill) {
            player.isStop = true;
            isSkill = true;

            StartCoroutine(nameof(SkillCoroutine));
        }
    }
    

    private IEnumerator SkillCoroutine() {

        int count = level;

        while (count-- > 0) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, skillRange, 1 << 3);
            if (colliders.Length == 0) break;

            float shortDistance = Vector3.Distance(transform.position, colliders[0].transform.position);
            Vector3 shortEnemyPos = colliders[0].transform.transform.position;

            // temptemptemptemptemptemptemptemp
            GameObject temp = colliders[0].gameObject; ;
            // --------------------------------

            foreach (var collider in colliders) {
                if(shortDistance > Vector3.Distance(transform.position, collider.transform.position)
                    && collider.transform.position.z >= transform.position.z) {
                    shortEnemyPos = collider.transform.transform.position;

                    // temptemptemptemptemptemptemptemp
                    temp = collider.gameObject;
                    // --------------------------------
                }
            }

            if (shortEnemyPos.z >= transform.position.z) {
                transform.position = shortEnemyPos;
                // temptemptemptemptemptemptemptemp
                if (!(temp is null))
                    Destroy(temp);
                // --------------------------------
            }

            yield return new WaitForSeconds(skillDelay);
        }
        
            player.isStop = false;
            isSkill = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}