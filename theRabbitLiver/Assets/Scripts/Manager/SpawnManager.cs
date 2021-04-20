using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour {
    private static readonly int TILE_SPACING = 3;

    public LevelDesign[] _levelDesign;
    public LevelDesign levelDesign {
        get { return _levelDesign[level]; }
    }

    public GameObject coin;
    public GameObject heart;
    public GameObject tempTile;

    public int initTileCount;

    private GameObject tileParent;
    private GameObject planeParent;

    private int level;
    private int totalTileCount;
    private int currLevelMaxTileCount;

	private void Awake() {
        Singleton();
    }

    private void Start() {
        Destroy(tempTile);

        planeParent = new GameObject("Planes");
        tileParent = new GameObject("Tiles");

        for (int i = 0; i < initTileCount; ++i) {
            SpawnTile();
        }
    }

    public void SpawnTile() {

        try {
            if (levelDesign.tileCount <= currLevelMaxTileCount) {
                if (_levelDesign.Length - 1 > level) {
                    currLevelMaxTileCount = 0;
                    level++;
                }
                return;
            }
        } catch (IndexOutOfRangeException e) {
            Debug.LogError(e.StackTrace);
		}

        GameObject tileSet = new GameObject(totalTileCount.ToString());

        for (int i = totalTileCount; i < totalTileCount + TILE_SPACING; ++i) {
            GameObject tile = i % 2 == 0 ? levelDesign.tile.tileLight : levelDesign.tile.tileDark;
            Vector3 pos = new Vector3((i - totalTileCount) * 3, 0, totalTileCount * TILE_SPACING);

            Instantiate(tile, pos, Quaternion.identity, tileSet.transform);

            if (SpawnObject(levelDesign.trap, pos, ref levelDesign.trapCount, tileSet)) {}
            else if (SpawnObject(heart, pos, ref levelDesign.heartCount, tileSet)) {}
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
        totalTileCount++;
	}

    public bool SpawnObject(GameObject gameObject, Vector3 pos, ref float count, GameObject parent) {
        int random = UnityEngine.Random.Range(0, 100);
        if (random < (count / levelDesign.tileCount) * 33) {
            if (--count > 0) {
                Instantiate(gameObject, pos, Quaternion.identity, parent.transform);
                return true;
            }
        }
        return false;
    }

    public void RemoveTile() {
        if (tileParent.transform.childCount > 0)
            Destroy(tileParent.transform.GetChild(0).gameObject);

        if (planeParent.transform.childCount > 4)
            Destroy(planeParent.transform.GetChild(0).gameObject);
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