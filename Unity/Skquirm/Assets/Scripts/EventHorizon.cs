﻿using UnityEngine;
using System.Collections;

public class EventHorizon : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision c)
    {
<<<<<<< HEAD
        if (other.gameObject.CompareTag("Player")){
            //GlobalSetting.Instance.SendMessage("PlayerDefeated", other.gameObject);
            other.GetComponent<PlayerController>().TryToHurt(); //responsible for the coins too
=======
        
        if (c.gameObject.CompareTag("Player")){
            Vector3 direction = c.gameObject.transform.position - transform.position;
            direction.Normalize();
            c.gameObject.GetComponent<PlayerController>().TryToHurt();
            print("did it hit?");
            c.collider.attachedRigidbody.AddForce(direction * 10f, ForceMode.VelocityChange);
>>>>>>> song
        }
        if (c.gameObject.CompareTag("obstacle"))
        {
            gameObject.GetComponent<ObstacleSpawn>().currentNumOfObs -= 1;
<<<<<<< HEAD
            Destroy(other.gameObject);
        }
        //other.gameObject.SetActive(false);
        
=======
            Destroy(c.gameObject);
        }
        //other.gameObject.SetActive(false);
>>>>>>> song
    }
}
