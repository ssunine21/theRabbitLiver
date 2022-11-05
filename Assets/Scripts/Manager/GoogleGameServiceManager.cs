using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using System.Threading.Tasks;

public class GoogleGameServiceManager : MonoBehaviour {

    public string UID {
        get => _uid;
        set => _uid = value;
    }

    private string  _autoCode;
    private string _playerName;
    private string _uid;

    private void Start() {
#if UNITY_EDITOR
        //PlayerPrefs.DeleteAll();
#endif
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ManuallyAuthenticate() {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status) {
        Debug.Log($"<color=red>ProcessAuthentication : {status}</color>");
        if (status == SignInStatus.Success) {
            PlayGamesPlatform.Instance.RequestServerSideAccess(false, (code) => {
                _autoCode = code;
                FirebaseAuthAsync();
            });
        } else {
            Debug.Log($"<color=red>Guest Login</color>");
            DataManager.init.CloudData.FirstGuestLogin();
        }
    }

    private void FirebaseAuthAsync() {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(_autoCode);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {

            Debug.Log($"<color=red>FirebaseAuthAsync : {task}</color>");

            if (task.IsCanceled) {
                Debug.Log($"<color = #ff0000>task is Canceled</color>");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("FirebaseAuthAsync(): Isfaulted");
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.Log($"User signed in successfully : {newUser.DisplayName} ({newUser.UserId})");

            Firebase.Auth.FirebaseUser user = auth.CurrentUser;
            if (user != null) {
                _playerName = user.DisplayName;
                _uid = user.UserId;

                DataManager.init.CloudData.Load(_uid);
            }
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