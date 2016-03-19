using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour
{
    private ObstacleSpawn obstaclespawn;
    public Rigidbody rb; //rigidbody is being strange?
    private VortexForce vf;
    // Use this for initialization
    void Start()
    {
        vf = GetComponent<VortexForce>();
        obstaclespawn = GetComponent<ObstacleSpawn>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

            //this portion of the code comes into play after the meteor object lands onto the water, at which then it is harmless, simply acts as a wall for players
            //it works, but it doesnt look good atm. Uncomment the code to see the results
            if (gameObject.transform.position.y < 3.6F)
            {
                //this is to disable rag dolling after it hits the water/reaches below a certain point in Y-coordinate
                //rb.isKinematic = true;
                vf.whirlStrength = 0.5f;
                vf.pullStrength = 0.001f;
                //rb.detectCollisions = false;
            }

    }

    void OnCollisionEnter(Collision collision)
    {
        //collision case of an obstacle with the event horizon.
        /*if (collision.gameObject.tag == "EHorizon")
        {
            obstaclespawn.currentNumOfObs -= 1;
            print(obstaclespawn.currentNumOfObs);
        }*/
        //collision case of a meteor obstacle vs. player
        if (collision.gameObject.tag == "Player")
        {
            //absolutely no clue why a regular box will always deal damage on collision, it makes no sense. Maybe if we replace it with the duck model itd be fine.
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
                collision.gameObject.GetComponent<PlayerController>().TryToHurt();
            }else{

                // get reflection vector
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 reflVector = Vector3.Reflect (rb.velocity, collision.contacts[0].normal);
                // omit y-direction to avoid jumps and sinks
                reflVector.y = 0;
                reflVector.Normalize();
                rb.AddForce(10 * reflVector, ForceMode.Impulse);

            }
        }
    }
}
