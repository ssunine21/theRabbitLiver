using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class CloudData {
    static readonly private string USERS = "users";
    static readonly private string LEVEL = "level";
    static readonly public string COIN = "coin";
    static readonly private string COINPLUS = "coinPlus";
    static readonly private string HEARTPLUS = "heartPlus";
    static readonly private string PROTECTIONPLUS = "protectionPlus";
    static readonly private string HEART = "heart";
    static readonly private string PROTECTION = "protection";
    static readonly private string SKIP = "skip";
    static readonly public string SCORE = "score";

    public class SaveData {

        public int[] level;
        public int coin;
        public int coinPlus;
        public int heartPlus;
        public int protectionPlus;
        public int heart;
        public int protection;
        public int skip;
        public int score;

        public SaveData(int[] level, int coin, int coinPlus, int heartPlus, int protectionPlus, int heart, int protection, int skip, int score) {
            this.level = level;
            this.coin = coin;
            this.coinPlus = coinPlus;
            this.heartPlus = heartPlus;
            this.protectionPlus = protectionPlus;
            this.heart = heart;
            this.protection = protection;
            this.skip = skip;
            this.score = score;
        }

        public void Init() {
            this.level = new int[5] {0, 0, 0, 0, 0};
            this.coin = 0;
            this.coinPlus = 0;
            this.heartPlus = 0;
            this.protectionPlus = 0;
            this.heart = 0;
            this.protection = 0;
            this.skip = 0;
            this.score = 0;
        }
    }

    public class ItemProductInfo {
        public int itemLevel { get; set; }
        public int[] price { get; set; }

        private int[] _percentage;
        public int percentage {
            get {
                if (itemLevel >= _percentage.Length) {
                    itemLevel = _percentage.Length - 1;
                }
                return _percentage[itemLevel]; 
            }
        }

        public int count;

        public ItemProductInfo(int itemLevel, int[] price, int[] percentage, int count = 1) {
            this.itemLevel = itemLevel;
            this.price = price;
            this._percentage = percentage;
            this.count = count;
        }
    }


    private DatabaseReference databaseReference;

    public readonly Dictionary<DeviceData.CharacterID, int> characterLevel = new Dictionary<DeviceData.CharacterID, int>();
    public readonly Dictionary<DeviceData.ItemID, ItemProductInfo> itemProductInfoList = new Dictionary<DeviceData.ItemID, ItemProductInfo>();

    private int _coin;
    public int coin {
        get {
            return _coin;
        } set {
            if (value < 0) value = 0;
            _coin = value;
        }
    }

    public CloudData() {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    SaveData loadData = null;
    public void Load(string _uid) {
        string originUid = null;
        string newUid = null;

        if (PlayerPrefs.HasKey(Definition.KEY_UID)) {
            originUid = DataManager.init.DeviceData.GetUID();
            if(_uid != originUid) {
                newUid = _uid;
                _uid = originUid;
            }
        }

        FirebaseDatabase.DefaultInstance.GetReference(USERS).Child(_uid)
        .GetValueAsync().ContinueWith(task => {

            if (task.IsFaulted) {
                Debug.Log($"<color=red>Haven't Id in firebase. : {_uid}</color>");
                loadData = GetInitData();
            } else if (task.IsCompleted) {

                Debug.Log($"<color=blue>Have Id in firebase. : {_uid}</color>");
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value != null) {
                    IDictionary data = (IDictionary)snapshot.Value;

                    IList collection = (IList)data[LEVEL];
                    int[] level = new int[5];

                    int i = 0;
                    foreach (var d in collection) {
                        level[i] = int.Parse(d.ToString());
                        i++;
                    }

                    loadData = new SaveData(
                        level,
                        int.Parse(data[COIN].ToString()),
                        int.Parse(data[COINPLUS].ToString()),
                        int.Parse(data[HEARTPLUS].ToString()),
                        int.Parse(data[PROTECTIONPLUS].ToString()),
                        int.Parse(data[HEART].ToString()),
                        int.Parse(data[PROTECTION].ToString()),
                        int.Parse(data[SKIP].ToString()),
                        int.Parse(data[SCORE].ToString())
                        );
                } else {
                    loadData = GetInitData();
                }
            }

            Debug.Log($"<color=blue>Loading Data : {_uid}</color>");
            try {
                LoadCharacterProductInfo();
                LoadItemProductInfo();
                LoadCoinData();
                LoadScore();

                if (!PlayerPrefs.HasKey(Definition.KEY_INIT)) {
                    Save(_uid);
                    PlayerPrefs.SetString(Definition.KEY_INIT, "false");
                }

                if(newUid != null) {
                    databaseReference.Child(USERS).Child(_uid).RemoveValueAsync();

                    Save(newUid);
                    PlayerPrefs.SetString(Definition.KEY_UID, newUid);
                }

            } catch (Exception e) {
                Debug.LogError(e.Message);
            }

            Debug.Log($"<color=blue>Load Data : {_uid}</color>");
            UIManager.init.isInitGame = true;
        });
    }

    public void FirstGuestLogin() {
        string uid;
        if (PlayerPrefs.HasKey(Definition.KEY_UID)) {
            uid = DataManager.init.DeviceData.GetUID();
        }
        else {
            uid = databaseReference.Child(USERS).Push().Key;
            GoogleGameServiceManager.init.UID = uid;
            PlayerPrefs.SetString(Definition.KEY_UID, uid);
        }

        Debug.Log($"<color=blue>Guest Login</color>");
        Load(uid);
    }

    private DatabaseReference GetFirebaseDataBase() {
        return FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void HandleValueChanged(object sender, ValueChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
    }

    public void Save(string _uid) {
        Debug.Log($"<color=blue>Saving Data : {_uid}</color>");
        SaveData saveData = new SaveData(
            characterLevel.Values.ToArray(),
            coin,
            itemProductInfoList[DeviceData.ItemID.coinPlus].itemLevel,
            itemProductInfoList[DeviceData.ItemID.heartPlus].itemLevel,
            itemProductInfoList[DeviceData.ItemID.protectionPlus].itemLevel,
            itemProductInfoList[DeviceData.ItemID.heart].count,
            itemProductInfoList[DeviceData.ItemID.protection].count,
            itemProductInfoList[DeviceData.ItemID.skip].count,
            DataManager.init.score.bestScore
            );

        string json = JsonUtility.ToJson(saveData);
        Debug.Log($"<color=blue>Save Data : {_uid}</color>");
        databaseReference.Child(USERS).Child(_uid).SetRawJsonValueAsync(json);

        PlayerPrefs.SetString(Definition.KEY_UID, _uid);
    }

    public void LevelUpAndAysnc(DeviceData.CharacterID characterID) {
        characterLevel[characterID] += 1;
        databaseReference.Child(USERS).Child(GoogleGameServiceManager.init.UID).Child(LEVEL).Child(((int)characterID).ToString()).SetValueAsync(characterLevel[characterID]);
    }

    public void DataAysnc(string Child, int value) {
        databaseReference.Child(USERS).Child(GoogleGameServiceManager.init.UID).Child(Child).SetValueAsync(value);
    }

    private SaveData GetInitData() {
        return new SaveData(
                        level: new int[5] { 1, 0, 0, 0, 0 },
                        coin: 1000000,
                        coinPlus: 0,
                        heartPlus: 0,
                        protectionPlus: 0,
                        heart: 1,
                        protection: 1,
                        skip: 1,
                        score: 0);
    }

    private void LoadCharacterProductInfo() {
        
        characterLevel.Add(DeviceData.CharacterID.bunny, loadData.level[0]);
        characterLevel.Add(DeviceData.CharacterID.skeleton, loadData.level[1]);
        characterLevel.Add(DeviceData.CharacterID.bono, loadData.level[2]);
        characterLevel.Add(DeviceData.CharacterID.jazz, loadData.level[3]);
        characterLevel.Add(DeviceData.CharacterID.notake, loadData.level[4]);
    }

    private void LoadItemProductInfo() {
        itemProductInfoList.Add(DeviceData.ItemID.coinPlus,
            new ItemProductInfo(
                itemLevel:loadData.coinPlus,
                price:new int[5] { 1000, 2000, 3000, 4000, 5000 },
                percentage:new int[5] { 20, 25, 30, 35, 40 }));

        itemProductInfoList.Add(DeviceData.ItemID.heartPlus,
            new ItemProductInfo(
                itemLevel:loadData.heartPlus,
                price: new int[5] { 1000, 2000, 3000, 4000, 5000 },
                percentage:new int[5] { 20, 25, 30, 35, 40 }));

        itemProductInfoList.Add(DeviceData.ItemID.protectionPlus,
            new ItemProductInfo(
                itemLevel:loadData.protectionPlus,
                price:new int[5] { 1000, 2000, 3000, 4000, 5000 },
                percentage:new int[5] { 5, 10, 12, 15, 17 }));

        itemProductInfoList.Add(DeviceData.ItemID.heart,
            new ItemProductInfo(
                itemLevel:1, 
                price:new int[5] { 1000, 2000, 3000, 4000, 5000 }, 
                percentage:new int[5] { 5, 10, 12, 15, 17 } ,
                count:loadData.heart));

        itemProductInfoList.Add(DeviceData.ItemID.protection,
            new ItemProductInfo(
                itemLevel:1,
                price:new int[5] { 1000, 2000, 3000, 4000, 5000 },
                percentage:new int[5] { 5, 10, 12, 15, 17 },
                count:loadData.protection));

        itemProductInfoList.Add(DeviceData.ItemID.skip,
            new ItemProductInfo(
                itemLevel:1,
                price:new int[5] { 1000, 2000, 3000, 4000, 5000 },
                percentage:new int[5] { 5, 10, 12, 15, 17 },
                count:loadData.skip));
    }

    private void LoadCoinData() {
        coin = loadData.coin;
    }

    private void LoadScore() {
        DataManager.init.score.bestScore = loadData.score;
    }
}