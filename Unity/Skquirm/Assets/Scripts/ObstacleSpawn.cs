using UnityEngine;
using System.Collections;

public class ObstacleSpawn : MonoBehaviour {
    public int maxNumOfObs;
    public float remainingTime; 
    public GameObject obstacle;
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
                xVar = Random.Range(-7.0F, 7.0F);
                zVar = Random.Range(-6.0F, 6.0F);
                currentNumOfObs += 1;
                Instantiate(obstacle, new Vector3(xVar , 100F, zVar), Quaternion.identity);
                spawnTime = remainingTime;
            }
        }
	}
}
