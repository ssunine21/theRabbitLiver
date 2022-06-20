using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Jazz : Character, ICharacter {
    [Range(0, 20)] public float speed;
    [SerializeField] private ParticleSystem particleSkill;
    [SerializeField] private int SKILL_RANGE = 0;
    private int skillMoveDistance;
    private bool isSkillDuration = false;
    public float mpDecreaseSpeed;

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

            if (transform.position.z >= (targetPos.z - posErrorRange)) {
                StartCoroutine(nameof(SkillDuration));
                transform.position = targetPos;
                player.isGroggy = false;
                isUsingSkill = false;
            }
        }
    }

    private IEnumerator SkillDuration() {
        particleSkill.Play();
        isSkillDuration = true;
        while (player.stamina.mpBar > 0 ) {
            player.stamina.mpBar -= (mpDecreaseSpeed * Time.deltaTime); 
            yield return null;
        }
        isSkillDuration = false;
        particleSkill.Stop();
    }

    [System.Obsolete]
    private void UseSkill() {
        if (isSkillDuration) {
            GameObject neareastObject = FindNearestObjectByTag(Definition.TAG_ENEMY);
            if (neareastObject == null) return;

            DestroyObject(neareastObject);
        }
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
        this.transform.position = new Vector3(3, 0, 3);
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