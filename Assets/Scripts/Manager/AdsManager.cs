using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour {
    private readonly int GAMEOVER_REWARDED = 0;
    private readonly int EXTRACOINS_REWARDED = 1;

    private RewardedAd gameOverRewardedAd;
    private RewardedAd extraCoinsRewardedAd;
    private InterstitialAd interstitialAd;

    private int _interstitialAdShowCount;
    public int InterstitialAdShowCount {
        get { return _interstitialAdShowCount; }
        set {
            _interstitialAdShowCount = value;

            if (_interstitialAdShowCount < 0) {
                _interstitialAdShowCount = UnityEngine.Random.Range(1, 5);
            }
        }
    }

    public UnityEvent OnAdOpening;
    public UnityEvent OnAdClosed;
    public UnityEvent OnUserEarnedRewardGameOver;
    public UnityEvent OnUserEarnedRewardExtraCoins;

    private void Awake() {
        Singleton();
    }

    private void Start() {
        MobileAds.Initialize(initStatue => { });

        RewardedAds();
        InterstitialAds();
    }

    private void RewardedAds() {
        string adUnitId = "";
#if DEBUG
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
#endif

        this.gameOverRewardedAd = CreateAndLoadRewardedAd(adUnitId, GAMEOVER_REWARDED);
        this.extraCoinsRewardedAd = CreateAndLoadRewardedAd(adUnitId, EXTRACOINS_REWARDED);
    }

    private void InterstitialAds() {
        string adUnitId = "";
#if DEBUG
        adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
#endif

        this.interstitialAd = CreateAndLoadInterstitialAd(adUnitId);
    }

    private InterstitialAd CreateAndLoadInterstitialAd(string adUnitId) {
        InterstitialAd interstitialAd = new InterstitialAd(adUnitId);

        interstitialAd.OnAdOpening += (sender, arge) => {
            MonoBehaviour.print("OnAdOpened");
            OnAdOpening.Invoke();
        };
        interstitialAd.OnAdClosed += (sender, arge) => {
            MonoBehaviour.print("OnAdClosed");
            InterstitialAdShowCount -= 1;
            this.interstitialAd = CreateAndLoadInterstitialAd(adUnitId);

            if (!this.gameOverRewardedAd.IsLoaded())
            OnAdClosed.Invoke();
        };

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);

        return interstitialAd;
    }

    private RewardedAd CreateAndLoadRewardedAd(string adUnitId, int type) {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdOpening += (sender, args) => {
            MonoBehaviour.print("OnAdOpened");
            OnAdOpening.Invoke();
        };

        if (type == GAMEOVER_REWARDED) {
            rewardedAd.OnUserEarnedReward += (sender, args) => {
                MonoBehaviour.print("User earned Reward ad reward: " + args.Amount);
                OnUserEarnedRewardGameOver.Invoke();
            };
        } else if (type == EXTRACOINS_REWARDED) {
            rewardedAd.OnUserEarnedReward += (sender, args) => {
                MonoBehaviour.print("User earned Reward ad reward: " + args.Amount);
                OnUserEarnedRewardExtraCoins.Invoke();
            };
        }
        rewardedAd.OnAdClosed += (sender, args) => {
            MonoBehaviour.print("OnAdClosed");

            if (!this.gameOverRewardedAd.IsLoaded())
                this.gameOverRewardedAd = CreateAndLoadRewardedAd(adUnitId, GAMEOVER_REWARDED);

            if (!this.extraCoinsRewardedAd.IsLoaded())
                this.extraCoinsRewardedAd = CreateAndLoadRewardedAd(adUnitId, EXTRACOINS_REWARDED);

            OnAdClosed.Invoke();
        };

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    public bool ShowInterstitialAd() {
        if(this.interstitialAd != null && this.interstitialAd.IsLoaded()) {
            if (InterstitialAdShowCount == 0) {
                this.interstitialAd.Show();
                return true;
            } else {
                InterstitialAdShowCount--;
            }
        }

        return false;
    }

    public void GameOverRewarded() {
        if (this.gameOverRewardedAd != null && this.gameOverRewardedAd.IsLoaded()) {
            this.gameOverRewardedAd.Show();
        }
    }

    public void ExtraCoinRewarded() {
        if (this.extraCoinsRewardedAd != null && this.extraCoinsRewardedAd.IsLoaded()) {
            this.extraCoinsRewardedAd.Show();
        }
    }

    public static AdsManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}