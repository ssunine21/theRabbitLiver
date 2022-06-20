using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skeleton : Character, ICharacter {
    int SKILL_RANGE = 1;
    float HP_RECOVERY = 0.01f;

    [Range(0, 20)]
    public float speed;
    private int distance;

    private float initZPosWithSkill = 0;
    float ICharacter.hpDecreasing {
        get => hpDecreasingSpeed;
    }
    float ICharacter.mpIncreasing {
        get => mpIncreasing;
    }


    protected override void Start() {
        base.Start();
        distance = MOVE_OFFSET;
        PurchaseSetting();
    }

    private void PurchaseSetting() {
        levelPrice = new int[5] { 0, 1000, 2000, 3000, 4000 };
        purchasePrice = 1000;
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
        base.GameSetting();
        switch (level) {
            case 0:
            case 1:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                player.hitDelay = 4f;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.9f;
                player.hitDelay = 4f;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.8f;
                player.hitDelay = 6f;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.75f;
                player.hitDelay = 7f;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.7f;
                player.hitDelay = 8f;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                break;
        }
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