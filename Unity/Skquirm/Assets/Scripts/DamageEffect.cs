using UnityEngine;
using System.Collections;

public class DamageEffect : MonoBehaviour {
	private static int hitpoints;  
	void Start(){

	}

	void Update () {
		hitpoints = Health.life; 
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")){

			Health.life -=1; 
		}
	}


}
