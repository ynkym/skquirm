using UnityEngine;
using System.Collections;

public class BubbleBehavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Vector3 start_scale = Vector3.zero;

        iTween.ScaleFrom(gameObject, iTween.Hash("scale", start_scale, "easetype", "spring", "time", 0.5f));	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
