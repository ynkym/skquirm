using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public int life;        // The player's life.
	//public Material damaged;
	public int force; 
	public GameObject water;
	public Rigidbody rb;
	void Awake ()
	{
		 rb = GetComponent<Rigidbody>();
		life = 3;
	}


	void Update ()
	{

		if (life == 2){
			//Debug.Log("lol");
			gameObject.GetComponent<Renderer>().material.color = Color.red;
			//barSoap.renderer.GetComponent<Renderer>().material.color = Color.black;
		}

		if (life == 1){
			if(transform.position.y >= water.transform.position.y){
				//rigid.body.AddForce(transform.down * force * 10); 
				//transform.localPosition = new Vector3(0, 0, 0);
				//rb.AddForce(0, -15, 0);
				//rb.AddForce(transform.down * 10);
				rb.AddForce(new Vector3(0, transform.position.y , 0) * -10);
			}

		}

		if (life <= 0){
			Destroy(this.gameObject);
			//Application.LoadLevel("GameOver");
		}

	}
}