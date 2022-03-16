using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Jazz : Character {
    [Range(0, 20)] public float speed;
    [SerializeField] private ParticleSystem particleSkill;
    [SerializeField] private int SKILL_RANGE = 0;
    private int skillMoveDistance;
    private bool isSkillDuration = false;
    public float mpDecreaseSpeed;

    protected override void Start() {
        base.Start();
    }

    protected override void Ready() { }

    public override bool Skill() {
        if (!base.Skill()) return false;
        skillMoveDistance = level * MOVE_OFFSET;

        player.isGroggy = true;

        targetPos = transform.position;
        targetPos.z += skillMoveDistance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if (transform.position.z >= (targetPos.z - posErrorRange)) {
                StartCoroutine(nameof(SkillDuration));
                transform.position = targetPos;
                player.isGroggy = false;
                isUsingSkill = false;
            }
        }
    }

    private IEnumerator SkillDuration() {
        particleSkill.Play();
        isSkillDuration = true;
        while (player.stamina.mpBar > 0 ) {
            player.stamina.mpBar -= (mpDecreaseSpeed * Time.deltaTime); 
            yield return null;
        }
        isSkillDuration = false;
        particleSkill.Stop();
    }

    private void UseSkill() {
        if (isSkillDuration) {
            GameObject neareastObject = FindNearestObjectByTag(Definition.TAG_ENEMY);
            if (neareastObject is null) return;
        }
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
}