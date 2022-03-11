using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SpawnManager : MonoBehaviour {

    public LevelDesign[] _levelDesign;
    public LevelDesign levelDesign {
        get { return _levelDesign[level]; }
    }

    public GameObject[] characterPrefab;

    public int prepareTileCount;

    private GameObject tileParent;
    private GameObject planeParent;
    private GameObject player;

    private int level;
    private int totalTileCount;
    private int currLevelMaxTileCount;

    private List<int> objectSpawnIndex = new List<int>();

    private void Awake() {
        Singleton();
    }

    public void CreateTileMap() {

        planeParent = new GameObject("Planes");
        tileParent = new GameObject("Tiles");

        PrepareTileSpawn();
        TileUpDownAnimStart();
    }

    public void DestroyTileMap() {
        level = 0;
        totalTileCount = 0;
        currLevelMaxTileCount = 0;

        try {
            Destroy(planeParent);
            Destroy(tileParent);

        } catch (Exception e) {
            Debug.Log("Destroy TileMap Error ::" + e);
        }

        player = null;
    }

    private void PrepareTileSpawn() {
        for (int i = 0; i < prepareTileCount; ++i) {
            SpawnTile(false);
        }
    }

    private void Update() {

        if (player == null) return;
        int playerZPos = player.transform.position.z == 0 ? 0 : (int)player.transform.position.z / 3;
        int spawnCount = (playerZPos + prepareTileCount) - totalTileCount;

        if (spawnCount > 0) {
            for (int i = 0; i < spawnCount; ++i) {
                SpawnTile(true);
            }
        }
    }

    public GameObject SpawnPlayer() {
        player = Instantiate(characterPrefab[(int)DataManager.init.DeviceData.characterId]);
        player.GetComponent<Player>().stamina = FindObjectOfType(typeof(Stamina)) as Stamina;

        return player;
    }

    public void SpawnTile(bool withObject) {
        try {
            if (levelDesign.tile.tileCount <= currLevelMaxTileCount) {
                if (_levelDesign.Length - 1 > level) {
                    currLevelMaxTileCount = 0;
                    level++;
                    PrepareTileSpawn();
                }
                return;
            }
        } catch (IndexOutOfRangeException e) {
            Debug.LogError(e.StackTrace);
		}

        GameObject tileSet = new GameObject(totalTileCount.ToString());
        for (int i = totalTileCount; i < totalTileCount + Definition.TILE_SPACING; ++i) {
            GameObject tile = i % 2 == 0 ? levelDesign.tile.tileLight : levelDesign.tile.tileDark;
            Vector3 pos = new Vector3((i - totalTileCount) * 3, 0, totalTileCount * Definition.TILE_SPACING);
            Instantiate(tile, pos, Quaternion.identity, tileSet.transform);

            if (withObject) {
                foreach (var gameObject in levelDesign.enemy)
                    SpawnObject(gameObject, pos, tileSet);
                foreach (var gameObject in levelDesign.trap)
                    SpawnObject(gameObject, pos, tileSet);

                SpawnObject(levelDesign.coin, pos, tileSet);
                SpawnObject(levelDesign.heart, pos, tileSet);
            }

            //if (withObject) {
            //    bool spawnObj =
            //        SpawnObject(levelDesign.trap, pos, ref levelDesign.trapCount, tileSet) ?
            //        true : SpawnObject(levelDesign.enemy, pos, ref levelDesign.enemyCount, tileSet) ?
            //        true : SpawnObject(heart, pos, ref levelDesign.heartCount, tileSet) ?
            //        true : SpawnObject(coin, pos, ref levelDesign.coinCount, tileSet);
            //}
        }

        tileSet.transform.SetParent(tileParent.transform);

        //Spawn Plane
        if(totalTileCount % 10 == 0) {
            GameObject planeSet = new GameObject((totalTileCount * 0.1).ToString());

            Instantiate(levelDesign.plane, new Vector3(-16.4f, -0.7f, (totalTileCount * 3) + 13.5f), Quaternion.Euler(new Vector3(0, 90, 0)), planeSet.transform);
            Instantiate(levelDesign.plane, new Vector3(22.5f, -0.7f, (totalTileCount * 3) + 13.5f), Quaternion.Euler(new Vector3(0, 270, 0)), planeSet.transform);

            planeSet.transform.SetParent(planeParent.transform);
        }

        currLevelMaxTileCount++;
        if (totalTileCount++ > 50) RemoveTile();
	}

    public bool SpawnObject(LevelDesign.LevelObject levelObject, Vector3 pos, GameObject parent) {
        //현재 타일이 정해놓은 범위를 넘어가면
        if (levelObject.currRange < currLevelMaxTileCount) {
            levelObject.currRange += (levelDesign.tile.tileCount / levelObject.count);

            //범위 내에서 소환할 위치 랜덤 결정
            int minRandomTileNum = currLevelMaxTileCount * Definition.TILE_SPACING;
            int maxRandomTileNum = levelObject.currRange * Definition.TILE_SPACING;
            levelObject.tileNum = UnityEngine.Random.Range(minRandomTileNum, maxRandomTileNum);

            //혹시 그 자리에 뭐가 있으면 다음 자리
            foreach (var idx in objectSpawnIndex) {
                if (levelObject.tileNum == idx) levelObject.tileNum++;
            }
            objectSpawnIndex.Add(levelObject.tileNum);
            return false;
        }

        //오브젝트 생성되는 타이밍
        if(currLevelMaxTileCount == (objectSpawnIndex[0] / Definition.TILE_SPACING)) {
            try {
                if (levelObject.gameObject == null) return false;

                PositionCorrection(ref pos, levelObject.gameObject.GetComponent<ObjectInfo>().offset);
                Instantiate(levelObject.gameObject, pos, levelObject.gameObject.transform.rotation, parent.transform);
                return true;
            } catch(Exception e) {
                Debug.LogError(e);
            }
        }
        return false;
    }

    private void PositionCorrection(ref Vector3 currPos, Vector3 correctionPos) {
        switch (currPos.x) {
            case 0:
                currPos.x -= (correctionPos.x);
                break;
            case 6:
                currPos.x += (correctionPos.x);
                break;
        }

        currPos.y += correctionPos.y;
        currPos.z += correctionPos.z;
    }

    public void RemoveTile() {
        if (tileParent.transform.childCount > 0)
            Destroy(tileParent.transform.GetChild(0).gameObject);

        if (planeParent.transform.childCount > 5)
            Destroy(planeParent.transform.GetChild(0).gameObject);
    }

    public void TileUpDownAnimStart() {
        foreach (TileObject tile in tileParent.GetComponentsInChildren<TileObject>()) {
            tile.StartUpDownCoroutine();
        }
    }

    public static SpawnManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
}