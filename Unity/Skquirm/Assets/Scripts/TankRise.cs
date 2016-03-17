using UnityEngine;
using System.Collections;

public class TankRise : MonoBehaviour {
	float timeLeft = 60.0f;
	bool tankrise; 


	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {

		timeLeft -= Time.deltaTime;

		if(timeLeft > 0 )
		{
			tankrise = true ; 

			if(tankrise = true){
				//transform.localScale -= new Vector3(0, 0.0035F, 0);
				//transform.Translate(0, -0.0035F, 0);
				//transform.localScale += new Vector3(0, 0.0025F, 0);
				transform.Translate(0, +0.002F, 0);
				//VortexForce.waterHorizon -= 0.001F; 
			}
		}

		if( timeLeft <= 0 )
		{
			tankrise = false;
			if(tankrise = false){
				transform.localScale -= new Vector3(0, 0, 0);
				//VortexForce.waterHorizon -= 0; 
			}

		}

	}
}