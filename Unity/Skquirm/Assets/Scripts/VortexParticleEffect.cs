using UnityEngine;
using System.Collections;

public class VortexParticleEffect : MonoBehaviour {

  public GameObject arm;
  public int numArms;
  public float radius;
  // public float pullSpeed;
  // public float driftSpeed;

  private GameObject[] armObjects;

	// Use this for initialization
	void Start () {
    armObjects = new GameObject[numArms];

    // instantiate arms from prefab
    for (int i = 0; i < numArms; i++){
      Vector3 position = new Vector3(
        radius * Mathf.Sin(i * 2 * Mathf.PI / numArms),
        0,
        radius * Mathf.Cos(i * 2 * Mathf.PI / numArms)
        );
      armObjects[i] = (GameObject) Instantiate(arm, transform.position + position, Quaternion.identity);
      armObjects[i].transform.parent = transform;
    }
	}
}
