using UnityEngine;
using System.Collections;

public class SpeedDefenseItem : Item {

	public GameObject shooter_back;
	public GameObject projectile_prefab;

	public GameObject temp_projectile;

	// Use this for initialization
	void Start () {
		//Get the prefab
		projectile_prefab = Resources.Load("Prefabs/Mine", (typeof(GameObject))) as GameObject;

		//Get the shooter back gameobj
		shooter_back = GetComponent<PlayerController>().shooter_back;
	}

	override public Item CombineWith (string anotherType) {
		// ignore
		return this;
	}

	override public Item Activate () {
		Debug.Log("Used SD Item");
		// TODO: implement

		//Giving boost
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(GlobalSetting.Instance.speedItemThrust * transform.forward, ForceMode.Impulse);

		//Get the rotation of the car object as EulerAngles
		Vector3 temp_rotation = transform.rotation.eulerAngles;

		//Adjust the rotation of the projectile that will be generated
		//temp_rotation.x = 90f;

		//Instantiate
		temp_projectile = Instantiate(projectile_prefab, shooter_back.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;

		//Set projectile info
		temp_projectile.GetComponent<SpeedDefenseProjectile> ().SetInfo (null, GetComponent<PlayerController>());

		temp_projectile = null;

        return null;
	}
}
