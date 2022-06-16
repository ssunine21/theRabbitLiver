 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordData {
    public int score;
    public int coin;
    public int enemyKill;
    public int runCount;
    public int hitCount;
    public int itemCount;
    public int totalScore;

    public int TotalScore() {
        totalScore = 0;
        totalScore += score;
        totalScore += coin;
        totalScore += enemyKill * 15; // 900
        totalScore += runCount * 5;   //1200
        totalScore += hitCount * 100;  // 15 = 1500
        totalScore += itemCount * 10 ; //  250 + 250 + 375 = 900

        return totalScore;
    }
}
