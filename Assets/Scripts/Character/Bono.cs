using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bono : Character, ICharacter {
    [Range(0, 20)]
    public float speed;
    private int skillMoveDistance;
    //BONO : Bono uses skill what move forward.

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
        skillMoveDistance = level * MOVE_OFFSET;

        player.isGroggy = true;

        targetPos = transform.position;
        targetPos.z += skillMoveDistance;

        return true;
    }

    private void FixedUpdate() {
        if (isUsingSkill) {
            transform.Translate((targetPos - transform.position) * speed * Time.deltaTime);

            if(transform.position.z >= (targetPos.z - posErrorRange)) {
                transform.position = targetPos;
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
    public override void GameSetting() {
        base.GameSetting();
        switch (level) {
            case 0:
            case 1:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.9f;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.8f;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.75f;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.7f;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                break;
        }
    }

    public int SkillCount() {
        return 1;
    }

    public DeviceData.CharacterID CharacterType() {
        return _Type;
    }

    public string SetInfoMessage() {
        string message = "";

        switch (level) {
            case 0:
                message =
                    "\nü�� ���� ȿ��  <b><color=#50bcdf>- " + 11 + "%</color></b>\n" +
                    "��ų ���� Ƚ��  <b><color=#50bcdf>+ " + 11 + "</color></b>";
                break;
            case 1:
                message =
                    "\nü�� ���� ȿ��  <b><color=#50bcdf>- " + 22 + "%</color></b>\n" +
                    "��ų ���� Ƚ��  <b><color=#50bcdf>+ " + 11 + "</color></b>";
                break;
            case 2:
                message =
                    "\nü�� ���� ȿ��  <b><color=#50bcdf>- " + 33 + "%</color></b>\n" +
                    "��ų ���� Ƚ��  <b><color=#50bcdf>+ " + 11 + "</color></b>";
                break;
            case 3:
                message =
                    "\nü�� ���� ȿ��  <b><color=#50bcdf>- " + 44 + "%</color></b>\n" +
                    "��ų ���� Ƚ��  <b><color=#50bcdf>+ " + 11 + "</color></b>";
                break;
            case 4:
                message =
                    "\nü�� ���� ȿ��  <b><color=#50bcdf>- " + 55 + "%</color></b>\n" +
                    "��ų ���� Ƚ��  <b><color=#50bcdf>+ " + 11 + "</color></b>";
                break;
            default:
                message = "";
                break;
        }

        return message;
    }
}