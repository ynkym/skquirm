using UnityEngine;
using System.Collections;

public class TitleCamera : MonoBehaviour {

  public float radius = 10;
  public float orbitTime = 60;

  private Vector3 origin;

	// Use this for initialization
	void Start () {
    origin = new Vector3(0, 1, 0);
	}

	// Update is called once per frame
	void Update () {
    float period = 2 * Mathf.PI * Time.time / orbitTime;
    transform.position = new Vector3(radius * Mathf.Cos(period), transform.position.y, radius * Mathf.Sin(period));
    transform.LookAt(origin);
	}
}
