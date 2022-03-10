using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LevelDesign {

	public Tile tile;
	public GameObject plane;
	public LevelObject[] enemy;
	public LevelObject[] trap;
	public LevelObject[] coin;
	public LevelObject[] heart;

	[Range(0, 1)]
	public float hpDecreasingSpeed;


	[System.Serializable]
	public class LevelObject {
		public GameObject gameObject;
		[Range(0, 100)]
		public int count;

		[HideInInspector]
		public int currRange;
	}

	[System.Serializable]
	public class Tile {
		public GameObject tileLight;
		public GameObject tileDark;
		[Range(0, 100)]
		public int tileCount;
	}
}