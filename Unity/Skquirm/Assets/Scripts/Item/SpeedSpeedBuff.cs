using UnityEngine;
using System.Collections;

public class SpeedSpeedBuff : MonoBehaviour {

    public float interval = 5.0f;
    private float remainingTime;
    public PlayerController ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<PlayerController>();
        ps.speed *= 2;
        remainingTime = interval;
	}
	
	// Update is called once per frame
	void Update () {
        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime;
            if (remainingTime <= 0)
            {
                ps.speed /= 2;
                Destroy(this);
            }
        }
    }
}
