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
	public int force;
	public GameObject water;
	public Rigidbody rb;
	void Awake ()
	{
		 rb = GetComponent<Rigidbody>();
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
        invincible = true;
        //print("INVINCIBLE");
        yield return new WaitForSeconds(invincibilityFrames); //waits for 3 seconds before changing invincibility back to false
        invincible = false;
        //print("Not Invincible...");

    }

}