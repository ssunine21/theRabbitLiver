using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notake : Character {
    [Range(0, 20)]
    public float skillDelay;
    [Range(0, 20)]
    public float skillRange;

    public LayerMask layerMask;

    private Vector3 pivot;

    protected override void Start() {
        base.Start();
    }

    public override bool Skill() {
        if (!base.Skill()) return false;

        StartCoroutine(nameof(SkillCoroutine));
        return true;
    }
    
    private IEnumerator SkillCoroutine() {

        int count = level;

        float shortDistance;
        Vector3 targetEnemyPos;

        while (count-- > 0) {
            pivot = transform.position;
            pivot.x = 3;

            Collider[] colliders = Physics.OverlapSphere(pivot, skillRange, 1 << 3);
            if (colliders.Length == 0) break;

            shortDistance = skillRange;
            targetEnemyPos = Vector3.zero;

            // temptemptemptemptemptemptemptemp
            GameObject temp = null;
            // --------------------------------

            float distance;

            foreach (var collider in colliders) {
                distance = Vector3.Distance(transform.position, collider.transform.position);

                if(collider.transform.position.z >= transform.position.z) {
                    if (targetEnemyPos == Vector3.zero
                        || collider.transform.position.z < targetEnemyPos.z) {

                        shortDistance = distance;
                        targetEnemyPos = collider.transform.position;
                        temp = collider.gameObject;

                    } else if(collider.transform.position.z == targetEnemyPos.z) {

                        if (shortDistance > distance) {

                            shortDistance = distance;
                            targetEnemyPos = collider.transform.position;

                            // temptemptemptemptemptemptemptemp
                            temp = collider.gameObject;
                            // --------------------------------
                        }
                    }
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
        
            player.isStopping = false;
            isUsingSkill = false;
    }

    private void OnDrawGizmosSelected() {
        pivot = transform.position;
        pivot.x = 3;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pivot, skillRange);
    }
}