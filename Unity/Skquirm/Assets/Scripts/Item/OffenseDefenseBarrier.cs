﻿using UnityEngine;
using System.Collections;

public class OffenseDefenseBarrier : DefenseBarrier {

	// Use this for initialization
	void Start () {
		// Can use different prefab in the future
		GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Barrier.prefab", (typeof(GameObject))) as GameObject;
		barrier = (GameObject) Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
		barrier.transform.parent = gameObject.transform;
	}

}