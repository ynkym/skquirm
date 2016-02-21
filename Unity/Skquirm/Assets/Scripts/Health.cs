using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public int life;        // The player's life.

    private int currentLife;
    

	//public Material damaged;
	void Awake ()
	{
		life = 3;
	}

    void Start ()
    {
        currentLife = life;
    }

    void getDamaged ()
    {
        currentLife -= 1;
        if (currentLife <= 0)
        {
            //Application.LoadLevel("GameOver");
        }
    }

	void Update ()
	{

		if (currentLife == 2){
			//Debug.Log("lol");
			gameObject.GetComponent<Renderer>().material.color = Color.red;
			//barSoap.renderer.GetComponent<Renderer>().material.color = Color.black;
		}

	}
}