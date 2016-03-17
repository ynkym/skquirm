using UnityEngine;
using System.Collections;

public class ObstacleSpawn : MonoBehaviour {
    public int maxNumOfObs;
    public float remainingTime; 
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    private int pickint;
    private float xVar;
    private float zVar;
    private float spawnTime;
    public int currentNumOfObs;
	// Use this for initialization
	void Start () {
        spawnTime = remainingTime;
        currentNumOfObs = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentNumOfObs < maxNumOfObs)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                pickint = Random.Range(0,3);
                xVar = Random.Range(-37.0F, 25.0F);
                zVar = Random.Range(-28.0F, 25.0F);
                currentNumOfObs += 1;
                if (pickint == 0)
                {
                    Instantiate(obstacle1, new Vector3(xVar, 100F, zVar), Quaternion.identity);
                    spawnTime = remainingTime;
                }
                else if (pickint == 1)
                {
                    Instantiate(obstacle2, new Vector3(xVar, 100F, zVar), Quaternion.identity);
                    spawnTime = remainingTime;
                }
                else
                {
                    Instantiate(obstacle3, new Vector3(xVar, 100F, zVar), Quaternion.identity);
                    spawnTime = remainingTime;
                }
            }
        }
	}
}
