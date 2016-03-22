using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

    public GameObject shooter;
    public GameObject projectile_prefab;
    public GameObject oo_prefab;

    public PlayerController controller;

    GameObject temp_projectile;

    private float speed = 20f;
    private GameObject target = null;

	// Use this for initialization
	void Start () {

	}

  // set the parameter and wait for animation event.
  void EnterThrow(object[] storage){
    speed = (float)storage[0];
    target = (GameObject)storage[1];
  }

  // triggered by Animation Event.
  void ThrowEvent(){
    //Get the rotation of the car object as EulerAngles
    Vector3 temp_rotation = transform.rotation.eulerAngles;

    //Adjust the rotation of the projectile that will be generated
    temp_rotation.x = 90f;

    //Instantiate
    if (target == null){
      temp_projectile = Instantiate(projectile_prefab, shooter.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;
    }else{
      temp_projectile = Instantiate(oo_prefab, shooter.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;
    }

    //Give it a velocity
    temp_projectile.GetComponent<Rigidbody>().velocity = transform.forward * speed;

    //Set projectile info
    temp_projectile.GetComponent<Projectile> ().SetInfo (target, controller);
  }
}
