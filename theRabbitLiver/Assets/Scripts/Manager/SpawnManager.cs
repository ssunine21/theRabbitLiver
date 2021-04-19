using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour {
    private static readonly int tileSpacing = 3;

    public LevelDesign[] levelDesign;

    public GameObject coin;
    public GameObject heartItem;
    public GameObject tempTile;

    public int initTileCount;

    private GameObject tiles;

    private int level;
    private int jumpCount;
    private int currLevelMaxTileCount;

	private void Awake() {
        Singleton();
    }

    private void Start() {
        Destroy(tempTile);

        tiles = new GameObject("Tiles");
        for (int i = 0; i < initTileCount; ++i) {
            SpawnTile();
        }
    }

    public void SpawnTile() {

        try {
            if (levelDesign[level].tileCount < currLevelMaxTileCount) {
                if (levelDesign.Length - 1 > level) {
                    currLevelMaxTileCount = 0;
                    level++;
                }
                return;
            }
        } catch (IndexOutOfRangeException e) {
            Debug.LogError(e.StackTrace);
		}

        GameObject gameObject = new GameObject(jumpCount.ToString());

        for (int i = jumpCount; i < jumpCount + tileSpacing; ++i) {
            GameObject tile = i % 2 == 0 ? levelDesign[level].tile.tileLight : levelDesign[level].tile.tileDark;
            Vector3 pos = new Vector3((i - jumpCount) * 3, 0, jumpCount * tileSpacing);

            Instantiate(tile, pos, Quaternion.identity, gameObject.transform);

            if (SpawnObject(levelDesign[level].trap, pos, ref levelDesign[level].trapCount, gameObject)) {}
            else if (SpawnObject(heartItem, pos, ref levelDesign[level].heartCount, gameObject)) {}
        }
        gameObject.transform.SetParent(tiles.transform);

        currLevelMaxTileCount++;
        jumpCount++;
	}

    public bool SpawnObject(GameObject gameObject, Vector3 pos, ref float count, GameObject parent) {
        int random = UnityEngine.Random.Range(0, 100);
        if (random < (count / levelDesign[level].tileCount) * 33) {
            if (--count > 0) {
                Instantiate(gameObject, pos, Quaternion.identity, parent.transform);
                return true;
            }
        }
        return false;
    }

    public void RemoveTile() {
        if (tiles.transform.childCount > 0)
            Destroy(tiles.transform.GetChild(0).gameObject);
	}


	public static SpawnManager init;
    private void Singleton() {
        if (init is null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
}