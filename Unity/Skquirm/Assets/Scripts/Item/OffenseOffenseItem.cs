using UnityEngine;
using System.Collections;

public class OffenseOffenseItem : Item {

	public GameObject target;
	public GameObject shooter;
	GameObject projectile_prefab;

	GameObject temp_projectile;

	[SerializeField] float speed = 25f;
	[SerializeField] float max_time;

	PlayerController origin;

	// Use this for initialization
	void Start () {
		origin = GetComponent<PlayerController> ();

		projectile_prefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/OOProjectile.prefab", (typeof(GameObject))) as GameObject;
		//Find the shooter 
		shooter = origin.shooter;
	}

  override public Item CombineWith (string anotherType) {
    // ignore
    return this;
  }

	override public Item Activate () {
	    Debug.Log("Used OO Item");
	    // TODO: implement
		//Get the rotation of the car object as EulerAngles
		Vector3 temp_rotation = transform.rotation.eulerAngles;

		//Adjust the rotation of the projectile that will be generated
		temp_rotation.x = 90f;

		//Instantiate
		temp_projectile = Instantiate(projectile_prefab, shooter.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;

		//Get target
		target = GlobalSetting.Instance.ReturnTheNearest (this.gameObject);//return the nearest target

		//Give it a velocity
		temp_projectile.GetComponent<Rigidbody>().velocity = transform.rotation * Vector3.forward * speed;

		//Set projectile info
		temp_projectile.GetComponent<OffenseOffenseProjectile> ().SetInfo (target, origin);

		temp_projectile = null;

        return null;
  }


}
