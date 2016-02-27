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
          GlobalSetting.Instance.SendMessage("PlayerDefeated", other.gameObject.GetComponent<PlayerController>().playerNum);
        }
        other.gameObject.SetActive(false);
    }
}
