using UnityEngine;
using System.Collections;

public class OffenseItem : Item {

	//Item scripts get attached to the gameobject which is in the top of the hierarchy of the avatar structure

	public GameObject shooter;
	public GameObject projectile_prefab;

	GameObject temp_projectile;

	[SerializeField] float speed = 25f;
	[SerializeField] float max_time;

	void Start(){
		//Load projectile prefab
		projectile_prefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Capsule.prefab", (typeof(GameObject))) as GameObject;

		//Find the shooter
		shooter = GetComponent<PlayerController>().shooter;

	}

  override public Item CombineWith (string anotherType) {
    Item newitem = null;
    if (anotherType == "Defense"){
      newitem = gameObject.AddComponent<OffenseDefenseItem>() as OffenseDefenseItem;
    } else if (anotherType == "Offense"){
      newitem = gameObject.AddComponent<OffenseOffenseItem>() as OffenseOffenseItem;
    } else if (anotherType == "Speed"){
      newitem = gameObject.AddComponent<OffenseSpeedItem>() as OffenseSpeedItem;
    }

    if (newitem != null){
       Destroy(this);
       return newitem;
    }
    return this;
  }

  override public void Activate() {
  		Debug.Log("Used Offense Item");
  		// TODO: implement

		//Get the rotation of the car object as EulerAngles
		Vector3 temp_rotation = transform.rotation.eulerAngles;

		//Adjust the rotation of the projectile that will be generated
		//temp_rotation.x = 90f;

		//Instantiate
		temp_projectile = Instantiate(projectile_prefab, shooter.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;

		//Give it a velocity
		temp_projectile.GetComponent<Rigidbody>().velocity = transform.forward * speed;

		//Set projectile info
		temp_projectile.GetComponent<OffenseProjectile> ().SetInfo (null, GetComponent<PlayerController>());

		temp_projectile = null;

	}
}
