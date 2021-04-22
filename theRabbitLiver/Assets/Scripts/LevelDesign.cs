using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LevelDesign {

	public Tile tile;
	public GameObject plane;
	public GameObject enemy;
	public GameObject trap;

	[Range(0, 100)]
	public int tileCount;
	[Range(0, 100)]
	public int enemyCount;
	[Range(0, 100)]
	public int trapCount;
	[Range(0, 100)]
	public int coinCount;
	[Range(0, 100)]
	public int heartCount;

	[Range(0, 1)]
	public float hpDecreasingSpeed;
}

[System.Serializable]
public class Tile {
	public GameObject tileLight;
	public GameObject tileDark;
}