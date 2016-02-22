using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public int life;        // The player's life.

    private bool invincible = false;

    private int currentLife;

	public LifeStateUI lifeUI;

	//public Material damaged;
	void Awake ()
	{
		life = 3;
	}

    void Start ()
    {
        currentLife = life;
    }

    public IEnumerator getDamaged ()
    {
        if (!invincible)
        {
            currentLife -= 1;

            if (currentLife <= 0)
            {
                //Application.LoadLevel("GameOver");
                Debug.Log("GameOver");
            }
            else {
                lifeUI.UpdateUI(currentLife);
            }
            invincible = true;
            yield return new WaitForSeconds(3); //waits for 3 seconds before changing invincibility back to false
            invincible = false;
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