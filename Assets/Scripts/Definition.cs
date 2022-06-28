using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Definition {
    public static readonly int TILE_SPACING = 3;
    public static readonly int CAMERA_DEPTH_UNDER = -1;
    public static readonly int CAMERA_DEPTH_OVER = 1;

    public static readonly int PRICE_CHAR_BUNNY = 1000;
    public static readonly int PRICE_CHAR_SKELETON = 1000;
    public static readonly int PRICE_CHAR_BONO = 1000;
    public static readonly int PRICE_CHAR_NOTAKE = 1000;

    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_ATTACK_COLLIDER = "AttackCollider";
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_TRAP = "Trap";
    public static readonly string TAG_ITEM = "Item";
    public static readonly string TAG_HEALTH_ITEM = "HealthItem";

    public static readonly string SELECT = "선택";
    public static readonly string BUY = "구매하기";
    public static readonly string BUY_MASSAGE = "구매 하시겠습니까?";
    public static readonly string BUY_ENOUGH = "더 이상 구매할 수 없습니다.";
    public static readonly string BUY_LEVEL_MASSAGE = "레벨업 하시겠습니까?";
    public static readonly string NOT_ENOUGH_MONEY = "골드가 부족합니다.";
    public static readonly string CURR_ITEM_COUNT = "현재 개수 : ";

    public static readonly string HEALTH_SKILL_LEVEL = "체력 감소 속도 ";
    public static readonly string SKILL_SKILL_LEVEL = "스킬 충전 속도 ";
    public static readonly string HIT_DELAY = "타격 딜레이 감소  ";
    public static readonly string SKILL_DISTANCE = "스킬 거리 증가 ";


    //bunny
    public static readonly string SKILL_USE_COUNT = "스킬 사용 횟수 ";

    //skeleton
    public static readonly string AMOUNT_OF_BLOOD = "흡혈량 ";

    //jazz 
    public static readonly string SKILL_DURATION = "스킬 지속시간 ";

    //notake
    public static readonly string CONSECUTIVE_HIT_COUNT = "연속 타격 횟수 ";
}
