using UnityEngine;
using System.Collections;

public class DamageEffect : MonoBehaviour {
	void Start(){

	}

	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")){

			other.gameObject.GetComponent<Health>().life -= 1;
			//Health.life -=1;
		}
	}


}
