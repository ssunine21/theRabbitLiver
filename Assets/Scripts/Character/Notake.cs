using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Notake : Character, ICharacter {
    int SKILL_RANGE = 0;
    int CONSECUTIVE_HIT_COUNT = 0;

    float ICharacter.hpDecreasing {
        get => hpDecreasingSpeed;
    }
    float ICharacter.mpIncreasing {
        get => mpIncreasing;
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
        GameObject target = FindNearestObjectByTag(Definition.TAG_ENEMY);
        if (target != null) {
            if (!base.Skill()) return false;
            player.isGroggy = true;
            StartCoroutine(nameof(SkillCoroutine));
            return true;
        }
        return false;
    }

    IEnumerator SkillCoroutine() {
        for(int i = 0; i < CONSECUTIVE_HIT_COUNT; ++i) {
            GameObject target = FindNearestObjectByTag(Definition.TAG_ENEMY);
            if (target == null) break;

            player.animator.SetInteger("skillNum", Random.Range(0, 3));
            SoundManager.init.PlayPlayerSound(GetSkillSound());
            Vector3 targetPos = PosNormalize(target.transform.position);

            Destroy(target);

            this.transform.position = targetPos;
            player.stamina.hpBar += 0.06f;
            yield return new WaitForSeconds(.5f);
        }
        player.animator.SetInteger("skillNum", -1);

        isUsingSkill = false;
        player.isSuperCharge = false;
        player.isGroggy = false;
    }

    private Definition.SoundType GetSkillSound() {
        int idx = Random.Range(0, 4);
        switch (idx) {
            case 0:
                return Definition.SoundType.Skill_Notake_Zero;
            case 1:
                return Definition.SoundType.Skill_Notake_One;
            case 2:
                return Definition.SoundType.Skill_Notake_Two;
            case 3:
                return Definition.SoundType.Skill_Notake_Three;
        }

        return Definition.SoundType.Skill_Notake_Zero;
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
                hpDecreasingSpeed = 0.8f;
                player.hitDelay = 4f;

                CONSECUTIVE_HIT_COUNT = 2;
                SKILL_RANGE = 12;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.6f;
                player.hitDelay = 5f;

                SKILL_RANGE = 12;
                CONSECUTIVE_HIT_COUNT = 2;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.5f;
                player.hitDelay = 6f;

                SKILL_RANGE = 15;
                CONSECUTIVE_HIT_COUNT = 3;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.4f;
                player.hitDelay = 7f;

                SKILL_RANGE = 15;
                CONSECUTIVE_HIT_COUNT = 4;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.35f;
                player.hitDelay = 7.5f;

                SKILL_RANGE = 15;
                CONSECUTIVE_HIT_COUNT = 5;
                break;
            default:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 1f;
                player.hitDelay = 4f;

                SKILL_RANGE = 3;
                CONSECUTIVE_HIT_COUNT = 2;
                break;
        }
    }


    public string GetInfoMessage() {
        string message = "";

        switch (level) {
            case 0:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 1:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 2:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>+</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>++</color></b>\n";
                break;
            case 3:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>+</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            case 4:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>++</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>++++</color></b>\n";
                break;
            case 5:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>+++</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>+++++</color></b>\n";
                break;
            default:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+</color></b>\n" +

                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hitARow") + "<b><color=#50bcdf>+</color></b>\n";
                break;
        }

        return message;
    }


    public string GetContentMessage() {
        return LocalizationManager.init.GetLocalizedValue("notake_content");
    }
}