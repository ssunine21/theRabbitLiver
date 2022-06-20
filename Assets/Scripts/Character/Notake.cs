using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Notake : Character, ICharacter {
    int SKILL_RANGE = 0;
    int SKILL_COUNT = 5;

    [Range(0, 20)]
    public float skillDelay;
    public LayerMask layerMask;
    private Vector3 pivot;
      
    float ICharacter.hpDecreasing {
        get => hpDecreasingSpeed;
    }
    float ICharacter.mpIncreasing {
        get => mpIncreasing;
    }

    protected override void Start() {
        base.Start();

        levelPrice = new int[5] { 0, 1000, 2000, 3000, 4000 };
        purchasePrice = 1000;
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
    public int SkillLevel() {
        return level;
    }
    public int LevelPrice() {
        return levelPrice[level];
    }

    public int PurchasePrice() {
        return purchasePrice;
    }

    public void LevelUp() {
        level += 1;
    }

    public int SkillCount() {
        return 1;
    }

    public DeviceData.CharacterID CharacterType() {
        return _Type;
    }

    public override void GameSetting() {
        this.transform.position = new Vector3(3, 0, 3);
    }

    public string SetInfoMessage() {
        string message = "";

        switch (level) {
            case 0:
                message =
                    "\n체력 감소 효과  <b><color=#50bcdf>- " + 11 + "%</color></b>\n" +
                    "스킬 충전 횟수  <b><color=#50bcdf>+ " + 11 + "</color></b>";
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                message = "";
                break;
        }

        return message;
    }
}