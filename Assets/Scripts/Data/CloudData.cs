using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class CloudData {
    static readonly private string USERS = "users";
    static readonly private string LEVEL = "level";
    static readonly private string COIN = "coin";
    static readonly private string COINPLUS = "coinPlus";
    static readonly private string HEARTPLUS = "heartPlus";
    static readonly private string PROTECTIONPLUS = "protectionPlus";
    static readonly private string HEART = "heart";
    static readonly private string PROTECTION = "protection";
    static readonly private string SKIP = "skip";
    static readonly private string SCORE = "score";

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

    public void Start() {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        loadData = new SaveData(new int[5] { 1, 0, 0, 0, 0 }, 1000000, 0, 0, 0, 1, 1, 1, 0);

        LoadCharacterProductInfo();
        LoadItemProductInfo();
        LoadCoinData();
        LoadScore();

        //Load();
    }

    SaveData loadData = null;
    public bool Load() {
        string id = DataManager.init.GoogleId;

        FirebaseDatabase.DefaultInstance.GetReference(USERS).Child(id)
            //.ValueChanged += HandleValueChanged;
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {

                } else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Value != null) {
                        IDictionary data = (IDictionary)snapshot.Value;
                        Console.Write(int.Parse(data[COIN].ToString()));
                        Console.Write(snapshot.Child(LEVEL).Value as int[]);

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
                    }

                    LoadCharacterProductInfo();
                    LoadItemProductInfo();
                    LoadCoinData();
                    LoadScore();
                }
            });

        return loadData != null;
    }
    public void HandleValueChanged(object sender, ValueChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
    }

    public void Save() {
        string id = DataManager.init.GoogleId;
        SaveData saveData = new SaveData(
            characterLevel.Values.ToArray(),
            coin,
            itemProductInfoList[DeviceData.ItemID.coinplus].itemLevel,
            itemProductInfoList[DeviceData.ItemID.heartplus].itemLevel,
            itemProductInfoList[DeviceData.ItemID.protectionplus].itemLevel,
            itemProductInfoList[DeviceData.ItemID.heart].count,
            itemProductInfoList[DeviceData.ItemID.protection].count,
            itemProductInfoList[DeviceData.ItemID.skip].count,
            DataManager.init.score.bestScore
            );

        string json = JsonUtility.ToJson(saveData);
        databaseReference.Child(USERS).Child(id).SetRawJsonValueAsync(json);
    }

    private void LoadCharacterProductInfo() {
        
        characterLevel.Add(DeviceData.CharacterID.bunny, loadData.level[0]);
        characterLevel.Add(DeviceData.CharacterID.skeleton, loadData.level[1]);
        characterLevel.Add(DeviceData.CharacterID.bono, loadData.level[2]);
        characterLevel.Add(DeviceData.CharacterID.jazz, loadData.level[3]);
        characterLevel.Add(DeviceData.CharacterID.notake, loadData.level[4]);
    }

    private void LoadItemProductInfo() {
        itemProductInfoList.Add(DeviceData.ItemID.coinplus,
            new ItemProductInfo(loadData.coinPlus, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 20, 25, 30, 35, 40 }));
        itemProductInfoList.Add(DeviceData.ItemID.heartplus,
            new ItemProductInfo(loadData.heartPlus, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 20, 25, 30, 35, 40 }));
        itemProductInfoList.Add(DeviceData.ItemID.protectionplus,
            new ItemProductInfo(loadData.protectionPlus, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
        itemProductInfoList.Add(DeviceData.ItemID.heart,
            new ItemProductInfo(loadData.heart, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
        itemProductInfoList.Add(DeviceData.ItemID.protection,
            new ItemProductInfo(loadData.protection, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
        itemProductInfoList.Add(DeviceData.ItemID.skip,
            new ItemProductInfo(loadData.skip, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
    }

    private void LoadCoinData() {
        coin = loadData.coin;
    }

    private void LoadScore() {
        DataManager.init.score.bestScore = loadData.score;
    }
}