using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour
{
    private ObstacleSpawn obstaclespawn;
    public Rigidbody rb; //rigidbody is being strange?
    // Use this for initialization
    void Start()
    {
        obstaclespawn = GetComponent<ObstacleSpawn>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < 3.6F)
        {
            //this is to disable rag dolling after it hits the water/reaches below a certain point in Y-coordinate
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //collision case of an obstacle with the event horizon.
        if (collision.gameObject.tag == "EHorizon")
        {
            obstaclespawn.currentNumOfObs -= 1;
        }
        //collision case of a meteor obstacle vs. player
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.transform.position.y > 3.6F)
            {
                collision.gameObject.GetComponent<PlayerController>().TryToHurt();
            }
        }
    }
}
