using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using System.Threading.Tasks;

public class GoogleGameServiceManager : MonoBehaviour {

    public bool isLogin = false;

    private void Start() {
       // OnLogin();

#if UNITY_ANDROID
#endif
    }

    private void InitService() {
    }

    public void OnLogin() {

        //if(!Social.localUser.authenticated) {
            Social.localUser.Authenticate(callback => {
                if (callback) {
                    MonoBehaviour.print(Social.localUser.id);
                    DataManager.init.LoadToFirebase(Social.localUser.id);
                } else 
                    MonoBehaviour.print("½ÇÆÐ");
            });
        //}
    }

    public static GoogleGameServiceManager init;
    private void Awake() {
        Singleton();
    }
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}