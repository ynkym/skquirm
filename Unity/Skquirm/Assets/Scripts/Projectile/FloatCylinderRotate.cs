using UnityEngine;
using System.Collections;

public class FloatCylinderRotate : MonoBehaviour {
	float timeLeft = 60.0f;
	bool cylinderrotate; 
	bool cylinderswing;
	bool rotateback;
	void Update() {


		timeLeft -= Time.deltaTime;

		if(timeLeft <= 50 && timeLeft > 49 || timeLeft <= 40 && timeLeft > 39 || timeLeft <= 30 && timeLeft > 29 || timeLeft <= 20 && timeLeft > 19 || timeLeft <= 10 && timeLeft > 9)
		{
			cylinderrotate = true ; 

			if(cylinderrotate = true){

				//transform.Rotate(0,15,0);
				//transform.Rotate(Vector3.right * Time.deltaTime);
				//transform.Rotate(Vector3.right * 15, Space.World);
				//transform.rotation = Quaternion.Euler(0,0,-30 * Time.deltaTime * 45, Space.World);
				transform.Rotate(0,0,-35 * Time.deltaTime, Space.World);

			}
		}


		else if(timeLeft <= 48 && timeLeft > 47 || timeLeft <= 38 && timeLeft > 37 || timeLeft <= 28 && timeLeft > 27 || timeLeft <= 18 && timeLeft > 17 || timeLeft <= 8 && timeLeft > 7)
		{

			rotateback = true ; 

			if(cylinderrotate = true){
				transform.Rotate(0,0,35 * Time.deltaTime, Space.World);
			}

		}

		if(timeLeft <= 41 && timeLeft > 40){
			//transform.Rotate(Vector3.up * Time.deltaTime * 30, Space.World);
			transform.Rotate(0,0.45F,0 * Time.deltaTime, Space.World);
		}

		else if(timeLeft <= 39 && timeLeft > 38){
			transform.Rotate(0,-0.45F,0 * Time.deltaTime, Space.World);
		}

		if(timeLeft <= 21 && timeLeft > 20){
			transform.Rotate(0,-0.45F,0 * Time.deltaTime, Space.World);
		}

		else if(timeLeft <= 19 && timeLeft > 18){
			transform.Rotate(0,0.45F,0 * Time.deltaTime, Space.World);
		}
	}
}