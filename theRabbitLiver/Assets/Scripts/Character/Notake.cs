using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notake : Character {
    [Range(0, 20)]
    public float skillDelay;

    public float skillRange;

    public LayerMask layerMask;

    private new void Start() {
        base.Start();
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
            Vector3 pivot = transform.position;
            pivot.x = 3;

            Collider[] colliders = Physics.OverlapSphere(pivot, skillRange, 1 << 3);
            if (colliders.Length == 0) break;

            float shortDistance = skillRange;
            Vector3 targetEnemyPos = colliders[0].transform.position;

            // temptemptemptemptemptemptemptemp
            GameObject temp = colliders[0].gameObject;
            // --------------------------------

            float distance;

            foreach (var collider in colliders) {
                distance = Vector3.Distance(transform.position, collider.transform.position);

                if (shortDistance > distance
                    && collider.transform.position.z == transform.position.z) {

                    shortDistance = distance;
                    targetEnemyPos = collider.transform.position;

                    // temptemptemptemptemptemptemptemp
                    temp = collider.gameObject;
                    // --------------------------------
                }
            }

            if (targetEnemyPos == Vector3.zero) break;

            transform.position = targetEnemyPos;
            // temptemptemptemptemptemptemptemp
            if (!(temp is null))
                Destroy(temp);
            // --------------------------------

            yield return new WaitForSeconds(skillDelay);
        }
        
            player.isStop = false;
            isSkill = false;
    }

    private void OnDrawGizmosSelected() {
        Vector3 pivot = transform.position;
        pivot.x = 3;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pivot, skillRange);
    }
}