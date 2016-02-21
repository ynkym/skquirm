using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

  public GameObject offenseItem;
  public GameObject defenseItem;
  public GameObject speedItem;

  public int offenseRatio = 1;
  public int defenseRatio = 1;
  public int speedRatio = 1;

  public float spawnTime;

  private float remainingTime;
  private GameObject currentItem;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
    if (currentItem == null){
      remainingTime -= Time.deltaTime;
      if (remainingTime <= 0){
        GameObject prefab;
        // determine randomly which item to spawn
        float rand = Random.Range(0.0f, offenseRatio + defenseRatio + speedRatio);
        if (rand < offenseRatio){
          prefab = offenseItem;
        } else if (rand < offenseRatio + defenseRatio){
          prefab = defenseItem;
        }else{
          prefab = speedItem;
        }

        currentItem = (GameObject) Instantiate(prefab, transform.position, transform.rotation);
        currentItem.SendMessage("RegisterSpawner", gameObject);
      }
    }
	}

  void StartCount() {
    currentItem = null;
    remainingTime = spawnTime;
  }

  void OnDrawGizmos() {
    Gizmos.color = Color.magenta;
    Gizmos.DrawWireSphere(transform.position, 0.5f);
  }
}
