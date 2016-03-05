﻿using UnityEngine;
using System.Collections;

public class OffenseSpeedItem : Item {


	public GameObject shooter;
	public GameObject projectile_prefab;

	GameObject temp_projectile;

	[SerializeField] float speed = 40f;
	[SerializeField] float max_time;


	// Use this for initialization
	void Start () {
		//Load projectile prefab
		projectile_prefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Capsule.prefab", (typeof(GameObject))) as GameObject;

		//Find the shooter
		shooter = GetComponent<PlayerController>().shooter;
	}

  override public Item CombineWith (string anotherType) {
    // ignore
    return this;
  }

	override public Item Activate () {
		Debug.Log("Used OS Item");
		// TODO: implement

		//Get the rotation of the car object as EulerAngles
		Vector3 temp_rotation = transform.rotation.eulerAngles;

		//Adjust the rotation of the projectile that will be generated
		temp_rotation.x = 90f;

		//Instantiate
		temp_projectile = Instantiate(projectile_prefab, shooter.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;

		//Give it a velocity
		temp_projectile.GetComponent<Rigidbody>().velocity = transform.rotation * Vector3.forward * speed;

		//Set projectile info
		temp_projectile.GetComponent<OffenseProjectile> ().SetInfo (null, GetComponent<PlayerController>());

		temp_projectile = null;

        return null;
	}
}