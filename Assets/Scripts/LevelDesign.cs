using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LevelDesign {

	public Tile tile;
	public GameObject plane;
	public LevelObject[] enemy;
	public LevelObject[] trap;
	public LevelObject coin;
	public LevelObject heart;
	public LevelObject protection;

	[Range(0, 1)]
	public float hpDecreasingSpeed;

	public Definition.SoundType soundType;

	[System.Serializable]
	public class LevelObject {
		public GameObject gameObject;
		[Range(0, 100)]
		public int count;
		[Range(0, 100)]
		public int startOrder;

		//[HideInInspector]
		public int currRange;
		[HideInInspector]
		public int tileNum;
	}

	[System.Serializable]
	public class Tile {
		public GameObject tileLight;
		public GameObject tileDark;
		[Range(0, 300)]
		public int tileCount;
	}

	public void init() {
		foreach (var obj in enemy) {
			obj.currRange = 0;
		}
		foreach (var obj in trap) {
			obj.currRange = 0;
		}

		coin.currRange = 0;
		heart.currRange = 0;
		protection.currRange = 0;
	}
}