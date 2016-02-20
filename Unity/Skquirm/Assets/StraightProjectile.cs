using UnityEngine;
using System.Collections;

public class StraightProjectile : Projectile {


	GameObject explosion;

	// Use this for initialization
	void Start () {
		explosion = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Standard Assets/ParticleSystems/Prefabs/Explosion.prefab", (typeof(GameObject))) as GameObject;
	}
	
	void OnCollisionEnter(Collision collision){
		
		//Are we colliding with a player?
		if (collision.gameObject.tag == "Player") {
			// Push Player away
			collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*20f);

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
