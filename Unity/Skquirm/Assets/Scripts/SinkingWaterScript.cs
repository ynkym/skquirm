using UnityEngine;
using System.Collections;

public class SinkingWaterScript : MonoBehaviour {

    public GameObject fstLayer, sndLayer;

    public float fstReleaseYValue;
    public float sndReleaseYValue;
    public float sinkSpeed;

    bool triggered;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate() {
        if (triggered)
        {
            transform.Translate(Vector3.up * sinkSpeed);
            if( transform.position.y >= sndReleaseYValue && sndLayer.transform.parent == transform )
            {
                sndLayer.transform.parent = transform.parent;
            }
            else if(transform.position.y >= fstReleaseYValue && fstLayer.transform.parent == transform )
            {
                fstLayer.transform.parent = transform.parent;
            }
        }
    }

    [ContextMenu("Trigger Sinking Environment")]
    public void TriggerSinkingWater() {
        triggered = true;
    }
}
