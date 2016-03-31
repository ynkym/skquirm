using UnityEngine;
using System.Collections;

public class EventHorizon : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            //GlobalSetting.Instance.SendMessage("PlayerDefeated", other.gameObject);
            other.GetComponent<PlayerController>().TryToHurt(); //responsible for the coins too
        }
        if (other.gameObject.CompareTag("obstacle"))
        {
            gameObject.GetComponent<ObstacleSpawn>().currentNumOfObs -= 1;
            Destroy(other.gameObject);
        }
        //other.gameObject.SetActive(false);
        
    }
}
