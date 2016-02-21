using UnityEngine;
using System.Collections;

public class OffenseProjectile : Projectile {


	GameObject explosion;

	// Use this for initialization
	void Start () {
		explosion = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Standard Assets/ParticleSystems/Prefabs/Explosion.prefab", (typeof(GameObject))) as GameObject;
	}

	void OnCollisionEnter(Collision collision){
			Debug.Log("Collision");


		//Are we colliding with a player?
		if (collision.gameObject.tag == "Player") {

			if (collision.gameObject != origin.gameObject){
				// Push Player away
				collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*20f);

				// Cause Damage
				bool attackSuccess = collision.gameObject.GetComponent<PlayerController>().TryToHurt();

				if (attackSuccess){
					// Send the score to the player
					origin.IncreaseScore();
				}

				//Explosion particles
				GameObject temp_explosion = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy (temp_explosion, 6f);
				Destroy(gameObject);

			}
		}
		//Are we colliding with anything else?
		else {
			//What to do?
		}
	}
}
