using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Jazz : Character, ICharacter {
    [Range(0, 20)] public float speed;
    [SerializeField] private int SKILL_RANGE = 0;

    private ParticleSystem weaponParticle;
    private ParticleSystem attackParticle;
    private int skillMoveDistance;
    private float skillDurationTime = 0;
    private bool isSkillEffectContinue = false;
    public float mpDecreaseSpeed;

    float ICharacter.hpDecreasing {
        get => hpDecreasingSpeed;
    }
    float ICharacter.mpIncreasing {
        get => mpIncreasing;
    }

    private void Awake() {
        weaponParticle = GetComponentInChildren<ParticleSystem>();
        attackParticle = Resources.Load<ParticleSystem>("Particle/Jazz_attack");
    }

    protected override void Start() {
        base.Start();
        PurchaseSetting();
    }

    private void PurchaseSetting() {
        levelPrice = new int[5] { 0, 1000, 2000, 3000, 4000 };
        purchasePrice = 1000;
    }

    public override bool Skill() {
        if (!base.Skill()) return false;
        player.isGroggy = true;
        StartCoroutine(nameof(SkillDuration));
        targetPos = transform.position;
        targetPos.z += skillMoveDistance * MOVE_OFFSET;
        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);
             
            if (transform.position.z >= (targetPos.z - posErrorRange)) {
                transform.position = targetPos;
                player.isGroggy = false;
                player.isSuperCharge = false;
                isUsingSkill = false;
            }
        }

        if (isSkillEffectContinue) {
            GameObject enemyObj = FindNearestObjectByTag(Definition.TAG_ENEMY);
            if(enemyObj != null) {
                if (attackParticle != null) {
                    Instantiate(attackParticle, new Vector3(enemyObj.transform.position.x, 0.8f, enemyObj.transform.position.z), Quaternion.identity);
                }

                Destroy(enemyObj);
                player.stamina.hpBar += 0.04f;

            }
        }
    }

    private IEnumerator SkillDuration() {
        if(weaponParticle != null)
            weaponParticle.Play();

        isSkillEffectContinue = true;
        yield return new WaitForSeconds(skillDurationTime);
        isSkillEffectContinue = false;

        if (weaponParticle != null)
            weaponParticle.Stop();
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

                skillMoveDistance = 2;
                skillDurationTime = 2.5f;
                SKILL_RANGE = 6;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.9f;
                player.hitDelay = 5f;

                skillMoveDistance = 2;
                skillDurationTime = 2.5f;
                SKILL_RANGE = 6;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.8f;
                player.hitDelay = 6f;

                skillMoveDistance = 3;
                skillDurationTime = 3;
                SKILL_RANGE = 6;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.75f;
                player.hitDelay = 7f;

                skillMoveDistance = 3;
                skillDurationTime = 3.5f;
                SKILL_RANGE = 9;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.7f;
                player.hitDelay = 7.5f;

                skillMoveDistance = 3;
                skillDurationTime = 4.5f;
                SKILL_RANGE = 9;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                player.hitDelay = 4f;

                skillMoveDistance = 2;
                skillDurationTime = 2.5f;
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
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 1:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 2:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 3:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>++</color></b>\n";
                break;
            case 4:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            case 5:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            default:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DURATION + "<b><color=#50bcdf>+</color></b>\n";
                break;
        }

        return message;
    }
}