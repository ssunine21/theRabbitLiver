using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Definition {
    public enum SoundType {
        Intro,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        Stage6,
        Tik,
        Skill_Bunny,
        Skill_Bono,
        Skill_Skeleton,
        Skill_Jazz,
        Skill_Jazz_Effect,
        Skill_Notake_Zero,
        Skill_Notake_One,
        Skill_Notake_Two,
        Skill_Notake_Three,
        Item_Health,
        Item_Shield,
        Item_Skip,
        Item_Coin,
        Hit,
        Hitted,
        Hitted_Shield,
        Hitted_SnowBall,
        Enemy_Bee,
        Enemy_Lizard,
        Enemy_Scorpion,
        Enemy_Chandelier,
        Enemy_Muloc,
        Enemy_Coral,
        Enemy_Wolf,
        Enemy_Yeti,
        Enemy_Jellyfish,
        Enemy_Anglerfish,
        ButtonClick,
        ButtonClose,
        SFX_BUY,
        Move,
    }

    public static readonly int TILE_SPACING = 3;
    public static readonly int CAMERA_DEPTH_UNDER = -1;
    public static readonly int CAMERA_DEPTH_OVER = 1;

    public static readonly int PRICE_CHAR_BUNNY = 1000;
    public static readonly int PRICE_CHAR_SKELETON = 1000;
    public static readonly int PRICE_CHAR_BONO = 1000;
    public static readonly int PRICE_CHAR_NOTAKE = 1000;

    public static readonly string KEY_UID = "KEY_UID";
    public static readonly string KEY_INIT = "KEY_INIT";

    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_ATTACK_COLLIDER = "AttackCollider";
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_TRAP = "Trap";
    public static readonly string TAG_ITEM = "Item";
    public static readonly string TAG_HEALTH_ITEM = "HealthItem";

    public static readonly string GOOGLE_LOGIN_MESSAGE = "구글 계정으로 로그인 하시겠습니까?";
    public static readonly string GOOGLE_LOGIN_FAIL = "로그인에 실패 했습니다.";
}
