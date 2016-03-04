using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public int life;        // The player's life.

    private bool invincible = false;

    private int currentLife;

    public int invincibilityFrames;
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

    public IEnumerator getDamaged()
    {
        if (!invincible)
        {
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
                if (currentLife == 2 && bottleRenderer != null)
                {
                    //Debug.Log("lol");
                    bottleRenderer.material.color = Color.red;
                }
                else if (currentLife == 1 && bottleRenderer != null)
                {
                    bottleRenderer.material.color = Color.black;
                }

            }
        }
        invincible = true;
        //print("INVINCIBLE");
        yield return new WaitForSeconds(invincibilityFrames); //waits for 3 seconds before changing invincibility back to false
        invincible = false;
        //print("Not Invincible...");

    }
}