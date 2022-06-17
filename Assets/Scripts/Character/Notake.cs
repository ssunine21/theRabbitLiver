using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Notake : Character {
    int SKILL_RANGE = 0;
    int SKILL_COUNT = 5;

    [Range(0, 20)]
    public float skillDelay;

    public LayerMask layerMask;

    private Vector3 pivot;

    protected override void Start() {
        base.Start();
    }

    protected override void Ready() {

    }

    public override bool Skill() {
        if (!base.Skill()) return false;
        StartCoroutine(nameof(SkillCoroutine));
        return true;
    }

    IEnumerator SkillCoroutine() {
        for(int i = 0; i < SKILL_COUNT; ++i) {
            GameObject neareastObject = FindNearestObjectByTag(Definition.TAG_ENEMY);
            if (neareastObject == null) break;

            player.animator.SetInteger("skillNum", Random.Range(0, 3));
            Vector3 targetPos = PosNormalize(neareastObject.transform.position);
            this.transform.position = targetPos;
            yield return new WaitForSeconds(.5f);
        }
        player.animator.SetInteger("skillNum", -1);

        isUsingSkill = false;
        player.isGroggy = false;
    }

    private GameObject FindNearestObjectByTag(string tag) {
        SKILL_RANGE = level * Definition.TILE_SPACING;

        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();
        var neareastObject = objects
            .Where(obj => {
                return ((obj.transform.position.z >= transform.position.z)
                && (obj.transform.position.z <= transform.position.z + SKILL_RANGE));
            })
            .OrderBy(obj => {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        return neareastObject;
    }

    private Vector3 PosNormalize(Vector3 vector) {
        vector.x = vector.x - (vector.x % 3);
        vector.y = vector.y - (vector.y % 3);
        vector.z = vector.z - (vector.z % 3);
        return vector;
    }
}