using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    private static readonly int tileSpacing = 3;

    public LevelDesign levelDesign;

    public GameObject tileLight;
    public GameObject tileDark;
    public GameObject trap;
    public GameObject heartItem;
    public GameObject tempTile;

    public int initTileCount;

    private GameObject tiles;

    private int totalTileCount;

	private void Awake() {
        Singleton();
        levelDesign = new LevelDesign();
    }

    private void Start() {
        Destroy(tempTile);
        levelDesign.tileCount = 80;
        levelDesign.trapCount = 20;
        levelDesign.heartCount = 20;

        tiles = new GameObject("Tiles");
        for (int i = 0; i < initTileCount; ++i)
            SpawnTile();
    }

    public void SpawnTile() {
        if (levelDesign.tileCount < totalTileCount) return;

        GameObject gameObject = new GameObject(totalTileCount.ToString());

        for (int i = totalTileCount; i < totalTileCount + tileSpacing; ++i) {
            GameObject tile = i % 2 == 0 ? tileLight : tileDark;
            Vector3 pos = new Vector3((i - totalTileCount) * 3, 0, totalTileCount * tileSpacing);

            Instantiate(tile, pos, Quaternion.identity, gameObject.transform);

            if (SpawnObject(trap, pos, ref levelDesign.trapCount, gameObject)) { Debug.Log(levelDesign.trapCount); }
            else if (SpawnObject(heartItem, pos, ref levelDesign.heartCount, gameObject)) { Debug.Log(levelDesign.heartCount); }
        }
        gameObject.transform.SetParent(tiles.transform);

        totalTileCount++;
	}

    public bool SpawnObject(GameObject gameObject, Vector3 pos, ref float count, GameObject parent) {
        int random = Random.Range(0, 100);
        if (random < (count / levelDesign.tileCount) * 33) {
            if (--count > 0) {
                Instantiate(gameObject, pos, Quaternion.identity, parent.transform);
                return true;
            }
        }
        return false;
    }

    public void RemoveTile() {
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