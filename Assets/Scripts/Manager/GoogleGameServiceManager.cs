using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;

public class GoogleGameServiceManager : MonoBehaviour {
    private string authCode;
    private void Start() {
#if UNITY_ANDROID
        OnLogin();
        FirebaseAutenticate();
#endif
    }

    private void OnLogin() {
        if(!Social.localUser.authenticated) {
            Social.localUser.Authenticate(callback => {
                authCode = PlayGamesPlatform.Instance.GetUserId();
            });
        }
    }

    private void FirebaseAutenticate() {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential =
            Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled) {
                return;
            }
            if (task.IsFaulted) {
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
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