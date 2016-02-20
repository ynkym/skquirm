using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

    public GameObject shooter;
    public GameObject projectile_prefab;



    GameObject temp_projectile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.X))
        {
			//Get the rotation of the car object as EulerAngles
            Vector3 temp_rotation = transform.parent.transform.rotation.eulerAngles;

			//Adjust the rotation of the projectile that will be generated
            temp_rotation.x = 90f;
            
			//Instantiate
            temp_projectile = Instantiate(projectile_prefab, shooter.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;
            
			//Give it a velocity
			temp_projectile.GetComponent<Rigidbody>().velocity = transform.parent.transform.rotation * Vector3.forward * 25f;
            temp_projectile = null;

        }
	}
}
