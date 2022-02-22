using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skeleton : Character {
    int SKILL_RANGE = 0;
    float HP_RECOVERY = 0.01f;

    [Range(0, 20)]
    public float speed;
    private int distance;

    private float initZPosWithSkill = 0;


    protected override void Start() {
        base.Start();
        distance = MOVE_OFFSET;
    }

    private void Update() {
        if (isUsingSkill && initZPosWithSkill != this.transform.position.z) {
            isUsingSkill = false;
        }
    }

    protected override void Ready() {
    }

    public override bool Skill() {
        GameObject target = FindNearestObjectByTag(Definition.TAG_ENEMY);
        if (target != null) {
            if (!base.Skill()) return false;

            SKILL_RANGE = level * Definition.TILE_SPACING;

            player.isSuperCharge = false;

            Vector3 targetPos = PosNormalize(target.transform.position);

            this.transform.position = targetPos;
            initZPosWithSkill = this.transform.position.z;

            StartCoroutine(nameof(HPRecovery));
            return true;
        }
        return false;
    }

    private GameObject FindNearestObjectByTag(string tag) {
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

    IEnumerator HPRecovery() {
        while (isUsingSkill) {
            player.stamina.hpBar += HP_RECOVERY * level * Time.deltaTime;
            yield return null;
        }
    }

    public void OffUsingSkill() {
        isUsingSkill = false;
    }
}