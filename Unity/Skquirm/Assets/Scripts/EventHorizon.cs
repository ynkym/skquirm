﻿using UnityEngine;
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
        other.gameObject.SetActive(false);
    }
}
