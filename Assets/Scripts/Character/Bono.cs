using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bono : Character, ICharacter {
    private ParticleSystem particle;

    [Range(0, 20)]
    public float speed;
    public int skillMoveDistance;
    //BONO : Bono uses skill what move forward.

    float ICharacter.hpDecreasing {
        get => hpDecreasingSpeed;
    }
    float ICharacter.mpIncreasing {
        get => mpIncreasing;
    }

    private void Awake() {
        particle = GetComponentInChildren<ParticleSystem>();
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
        targetPos = transform.position;
        targetPos.z += skillMoveDistance * MOVE_OFFSET;
        
        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if(transform.position.z >= (targetPos.z - posErrorRange)) {
                transform.position = targetPos;
                player.isGroggy = false;
                isUsingSkill = false;
                player.isSuperCharge = false;

                if(particle != null) {
                    
                    particle.Play();
                }
            }
        }
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
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.9f;
                player.hitDelay = 5f;

                skillMoveDistance = 3;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.8f;
                player.hitDelay = 6f;

                skillMoveDistance = 3;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.75f;
                player.hitDelay = 7f;

                skillMoveDistance = 4;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.7f;
                player.hitDelay = 7.5f;

                skillMoveDistance = 4;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                player.hitDelay = 4f;

                skillMoveDistance = 2;
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
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 1:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 2:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>++</color></b>\n";
                break;
            case 3:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>++</color></b>\n";
                break;
            case 4:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            case 5:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            default:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_DISTANCE + "<b><color=#50bcdf>+</color></b>\n";
                break;
        }

        return message;
    }
}