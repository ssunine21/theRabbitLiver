using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeManager : MonoBehaviour {


    #region Singleton
    public static TestCodeManager init;
    private void Awake() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            GameManager.init.player.GetComponent<Player>().stamina.SetStamina(1, 1);
        }
    }

}