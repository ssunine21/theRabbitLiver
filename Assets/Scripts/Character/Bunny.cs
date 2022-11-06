using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Character, ICharacter {
    [Range(0, 20)]
    public float speed;
    private int distance = Definition.TILE_SPACING;
    private int skillCount = 1;

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
        if (!base.Skill()) return false;
        player.isGroggy = true;

        targetPos = transform.position;
        targetPos.z -= distance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if (transform.position.z <= (targetPos.z + posErrorRange)) {
                transform.position = targetPos;

                player.isSuperCharge = false;
                player.isGroggy = false;
                isUsingSkill = false;
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
        return skillCount;
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
                skillCount = 1;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.9f;
                player.hitDelay = 5f;
                skillCount = 1;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.8f;
                player.hitDelay = 6f;
                skillCount = 2;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.75f;
                player.hitDelay = 7f;
                skillCount = 2;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.7f;
                player.hitDelay = 7.5f;
                skillCount = 2;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                player.hitDelay = 4f;
                skillCount = 1;
                break;
        }
    }


    public string SetInfoMessage() {
        string message = "";

        switch (level) {
            case 0:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>";
                break;
            case 1:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+</color></b>";
                break;
            case 2:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>";
                break;
            case 3:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+</color></b>\n" +
                    Definition.SKILL_USE_COUNT + "<b><color=#50bcdf>+1</color></b>";
                break;
            case 4:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>++</color></b>\n" +
                    Definition.SKILL_USE_COUNT + "<b><color=#50bcdf>+1</color></b>";
                break;
            case 5:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.HIT_DELAY + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_USE_COUNT + "<b><color=#50bcdf>+1</color></b>";
                break;
            default:
                message = "";
                break;
        }

        return message;
    }
}