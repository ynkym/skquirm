using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public int life;        // The player's life.

    //private bool invincible = false;

    private int currentLife;

	public LifeStateUI lifeUI;
    public Renderer bottleRenderer;

	//public Material damaged;
	void Awake ()
	{
		life = 3;
	}

    void Start ()
    {
        currentLife = life;
    }

    public void getDamaged ()
    {
       // if (!invincible) **change void to IEnumerator if we're using this.
        //{
            currentLife -= 1;
        lifeUI.UpdateUI(currentLife);
        if (currentLife <= 0)
        {
            //Application.LoadLevel("GameOver");
            GlobalSetting.Instance.SendMessage("PlayerDefeated", this.gameObject);
            gameObject.SetActive(false);
            Debug.Log("GameOver");
        }
        else {        
            if (currentLife == 2)
            {
                //Debug.Log("lol");
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (currentLife == 1)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.black;
                //barSoap.renderer.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    
        //invincible = true;
        //yield return new WaitForSeconds(3); //waits for 3 seconds before changing invincibility back to false
        // invincible = false;
        //}
    }
}