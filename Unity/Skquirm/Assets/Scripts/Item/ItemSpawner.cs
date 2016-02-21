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

	[Header("Testing Settings")]
	[SerializeField] bool testing = false; //false as default. Disable it after testing
	[SerializeField] int firstSpawn;
	[SerializeField] int secondSpawn;
	bool firstSpawned;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (currentItem == null && !testing) {
			remainingTime -= Time.deltaTime;
			if (remainingTime <= 0) {
				GameObject prefab;
				// determine randomly which item to spawn
				float rand = Random.Range (0.0f, offenseRatio + defenseRatio + speedRatio);
			
				if (rand < offenseRatio) {
					prefab = offenseItem;
				} else if (rand < offenseRatio + defenseRatio) {
					prefab = defenseItem;
				} else {
					prefab = speedItem;
				}

				currentItem = (GameObject)Instantiate (prefab, transform.position, transform.rotation);
				currentItem.SendMessage ("RegisterSpawner", gameObject);
			}
		} else {
			if (currentItem == null ) {
				remainingTime -= Time.deltaTime;
				if (remainingTime <= 0) {
					GameObject prefab;
					// determine randomly which item to spawn

					if (!firstSpawned) {
						prefab = ReturnPrefabByIndex (firstSpawn);
						firstSpawned = true;
					} 
					else {
						prefab = ReturnPrefabByIndex (secondSpawn);
						firstSpawned = false;
					}

					currentItem = (GameObject)Instantiate (prefab, transform.position, transform.rotation);
					currentItem.SendMessage ("RegisterSpawner", gameObject);
				}
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

	GameObject ReturnPrefabByIndex(int index){
		switch (index) {
		case 0:
			return offenseItem;
			break;
		case 1:
			return defenseItem;
			break;
		case 2:
			return speedItem;
			break;
		}
		return null;
	}
}
