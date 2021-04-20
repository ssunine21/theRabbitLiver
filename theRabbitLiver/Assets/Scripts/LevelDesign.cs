using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LevelDesign {

	public Tile tile;
	public GameObject plane;
	public GameObject trap;

	[Range(0, 100)]
	public float tileCount;
	[Range(0, 100)]
	public float coinCount;
	[Range(0, 100)]
	public float heartCount;
	[Range(0, 100)]
	public float enemyCount;
	[Range(0, 100)]
	public float trapCount;

	[Range(0, 1)]
	public float hpDecreasingSpeed;
}

[System.Serializable]
public class Tile {
	public GameObject tileLight;
	public GameObject tileDark;
}