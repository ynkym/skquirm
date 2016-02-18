using UnityEngine;
using System.Collections;

public class OffenseOffenseProjectile : MonoBehaviour {

	PlayerController origin;
	GameObject target;

	Vector3 dist_vector;
	float angle_difference;

	GameObject explosion;

	// Use this for initialization
	void Start () {
		//Get the explosion prefab
		explosion = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Standard Assets/ParticleSystems/Prefabs/Explosion.prefab", (typeof(GameObject))) as GameObject;
	}

	void FixedUpdate () {
		Vector3 temp_direction;

		dist_vector = target.transform.position - transform.position;
		temp_direction = dist_vector;

		temp_direction.y = -90f;

		dist_vector = Vector3.Normalize (dist_vector);

		transform.rotation = Quaternion.LookRotation (temp_direction);
		GetComponent<Rigidbody>().velocity = 15f * dist_vector;

	}

	public void SetInfo(GameObject targetObj, PlayerController controller){
		target = targetObj;
		origin = controller;
	}

	void OnCollisionEnter(Collision collision){

		//Are we colliding with a player?
		if (collision.gameObject.tag == "Player") {
			// Push Player away
			collision.gameObject.GetComponent<Rigidbody>().AddForce(dist_vector*20f);

			// Cause Damage
			collision.gameObject.GetComponent<PlayerController>().Damage();

			// Send the score to the player
			origin.IncreaseScore();

		} 
		//Are we colliding with anything else?
		else {
			//What to do?
		}

		//Explosion particles
		GameObject temp_explosion = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity) as GameObject;

		Destroy (temp_explosion, 6f);
	}


}
