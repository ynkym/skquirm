using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public static int life;        // The player's life.
	//public Material damaged;
	void Awake ()
	{
		life = 3;
	}


	void Update ()
	{

		if (life == 2){
			Debug.Log("lol");
			gameObject.GetComponent<Renderer>().material.color = Color.red;
			//barSoap.renderer.GetComponent<Renderer>().material.color = Color.black;
		}

		if (life <= 0){

			//Application.LoadLevel("GameOver");
		}

	}
}  