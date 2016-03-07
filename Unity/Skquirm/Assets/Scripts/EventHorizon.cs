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
          GlobalSetting.Instance.SendMessage("PlayerDefeated", other.gameObject);
        }
        if (other.gameObject.CompareTag("obstacle"))
        {
            gameObject.GetComponent<ObstacleSpawn>().currentNumOfObs -= 1;
        }
        //other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }
}
