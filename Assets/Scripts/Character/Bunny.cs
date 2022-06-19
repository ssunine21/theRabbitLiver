using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Character, ICharacter {
    [Range(0, 20)]
    public float speed;
    private int distance = Definition.TILE_SPACING;

    protected override void Start() {
        base.Start();
        distance = MOVE_OFFSET;

        levelPrice = new int[5] { 0, 1000, 2000, 3000, 4000 };
        purchasePrice = 1000;
    }

    protected override void Ready() { }

    public override bool Skill() {
        if (!base.Skill()) return false;
        player.isGroggy = true;

        targetPos = transform.position;
        targetPos.z -= distance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * speed);
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
    public int LevelPrice() {
        return levelPrice[level];
    }

    public int PurchasePrice() {
        return purchasePrice;
    }

    public void LevelUp() {
        level += 1;
    }

    public DeviceData.CharacterID CharacterType() {
        return _Type;
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
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++</color></b>";
                break;
            case 3:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++</color></b>\n" +
                    "스킬 사용 횟수 " + "<b><color=#50bcdf>+1</color></b>";
                break;
            case 4:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>++++</color></b>\n" +
                    "스킬 사용 횟수 " + "<b><color=#50bcdf>+1</color></b>";
                break;
            case 5:
                message =
                    Definition.HEALTH_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    Definition.SKILL_SKILL_LEVEL + "<b><color=#50bcdf>+++++</color></b>\n" +
                    "스킬 사용 횟수 " + "<b><color=#50bcdf>+2</color></b>";
                break;
            default:
                message = "";
                break;
        }

        return message;
    }
}