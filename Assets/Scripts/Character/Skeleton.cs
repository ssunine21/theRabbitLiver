using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skeleton : Character, ICharacter {
    public ParticleSystem bloodParticle;

    public int SKILL_RANGE = 3;
    public float HP_RECOVERY;

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
            player.isGroggy = true;
            player.isSuperCharge = false;
            Vector3 targetPos = PosNormalize(target.transform.position);
            Destroy(target);

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
        if (bloodParticle != null)
            bloodParticle.Play();

        while (isUsingSkill) {
            if (player.hittingByTrap) {
                isUsingSkill = false;
            }
            player.stamina.hpBar += (HP_RECOVERY * Time.deltaTime);
            yield return null;
        }

        if (bloodParticle != null)
            bloodParticle.Stop();
    }

    public void OffUsingSkill() {
        isUsingSkill = false;
        player.isGroggy = false;
    }

    public int SkillLevel() {
        return level;
    }
    public void SetSkillLevel(int level) {
        this.level = level;
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

                HP_RECOVERY = 0.07f;
                SKILL_RANGE = 6;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.9f;
                player.hitDelay = 4f;

                HP_RECOVERY = 0.08f;
                SKILL_RANGE = 6;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.8f;
                player.hitDelay = 6f;

                HP_RECOVERY = 0.08f;
                SKILL_RANGE = 6;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.75f;
                player.hitDelay = 7f;

                HP_RECOVERY = 0.09f;
                SKILL_RANGE = 9;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.7f;
                player.hitDelay = 8f;

                HP_RECOVERY = 0.09f;
                SKILL_RANGE = 9;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                player.hitDelay = 4f;

                HP_RECOVERY = 0.09f;
                SKILL_RANGE = 6;
                break;
        }
    }

    public string SetInfoMessage() {
        string message = "";

        switch (level) {
            case 0:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 1:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 2:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 3:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 4:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 5:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n";
                break;
            default:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.AMOUNT_OF_BLOOD + "<b><color=#50bcdf>+</color></b>\n";
                break;
        }

        return message;
    }
}