using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LevelDesign {

	public Tile tile;
	public GameObject trap;

	public float tileCount;
	public float coinCount;
	public float heartCount;
	public float enemyCount;
	public float trapCount;

}

[System.Serializable]
public class Tile {
	public GameObject tileLight;
	public GameObject tileDark;
}