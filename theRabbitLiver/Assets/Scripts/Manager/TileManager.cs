using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    private static readonly int tileSpacing = 3;

    public GameObject tileLight;
    public GameObject tileDark;
    public GameObject tempTile;

    public int initTileCount;

    private GameObject tiles;

    private int totalTileCount;

	private void Awake() {
        Singleton(); 
    }

	private void Start() {
        Destroy(tempTile);

        tiles = new GameObject("Tiles");
        for (int i = 0; i < initTileCount; ++i)
            SpawnTile();
	}

    public void SpawnTile() {
        GameObject gameObject = new GameObject(totalTileCount.ToString());
        
        for(int i = totalTileCount; i < totalTileCount + tileSpacing; ++i) {
            GameObject tempTile = i % 2 == 0 ? tileLight : tileDark;

            Instantiate(
                tempTile, 
                new Vector3((i - totalTileCount) * 3, 0, totalTileCount * tileSpacing), 
                Quaternion.identity, 
                gameObject.transform
                );
		}

        gameObject.transform.SetParent(tiles.transform);
        totalTileCount++;
	}

    public void RemoveTile() {
        Destroy(tiles.transform.GetChild(0).gameObject);
	}


	public static TileManager init;
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