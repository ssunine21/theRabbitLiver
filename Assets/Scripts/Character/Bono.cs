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

        SoundManager.init.PlayPlayerSound(Definition.SoundType.Skill_Bono);

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

                    SoundManager.init.PlayPlayerSound(Definition.SoundType.Skill_Bono);
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
                hpDecreasingSpeed = 0.7f;
                player.hitDelay = 4f;

                skillMoveDistance = 2;
                break;
            case 2:
                mpIncreasing = 0f;
                hpDecreasingSpeed = 0.6f;
                player.hitDelay = 5f;

                skillMoveDistance = 3;
                break;
            case 3:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.5f;
                player.hitDelay = 6f;

                skillMoveDistance = 3;
                break;
            case 4:
                mpIncreasing = 0.05f;
                hpDecreasingSpeed = 0.45f;
                player.hitDelay = 7f;

                skillMoveDistance = 4;
                break;
            case 5:
                mpIncreasing = 0.15f;
                hpDecreasingSpeed = 0.4f;
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

    public string GetInfoMessage() {
        string message = "";

        switch (level) {
            case 0:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 1:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+</color></b>\n";
                break;
            case 2:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>++</color></b>\n";
                break;
            case 3:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>++</color></b>\n";
                break;
            case 4:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            case 5:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+++++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("hittedDelayDecrease") + "<b><color=#50bcdf>+++</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+++</color></b>\n";
                break;
            default:
                message =
                    LocalizationManager.init.GetLocalizedValue("hpDecreaseSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillChargeSpeed") + "<b><color=#50bcdf>+</color></b>\n" +
                    LocalizationManager.init.GetLocalizedValue("skillDistanceIncrease") + "<b><color=#50bcdf>+</color></b>\n";
                break;
        }

        return message;
    }

    public string GetContentMessage() {
        return LocalizationManager.init.GetLocalizedValue("bono_content");
    }
}