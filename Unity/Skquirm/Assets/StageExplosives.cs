using UnityEngine;
using System.Collections;

public class StageExplosives : Projectile
{

    GameObject explosion;

    void Start()
    {
        explosion = Resources.Load("Effects/Explosion", (typeof(GameObject))) as GameObject;
    }

    void OnCollisionEnter(Collision collision)
    {

        //Are we colliding with a player?
        if (collision.gameObject.tag == "Player")
        {
            print("Am I colliding?");

            // Push Player away
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 20f);

            // Cause Damage
            collision.gameObject.GetComponent<PlayerController>().TryToHurt();

        }
        //Are we colliding with anything else?
        else {
            //What to do?
        }

        //Explosion particles
        GameObject temp_explosion = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity) as GameObject;

        Destroy(temp_explosion, 6f);
        Destroy(gameObject);
    }
}
