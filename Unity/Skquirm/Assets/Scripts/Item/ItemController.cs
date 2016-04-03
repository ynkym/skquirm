using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

  public string itemType;
  private GameObject spawner;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


	}

  void RegisterSpawner(GameObject s){
    spawner = s;
  }

  void OnTriggerEnter(Collider other) {
    // If collided with player, send message to the item component of the
    // player to handle picking up / combination
    if (other.gameObject.CompareTag("Player")){
      // notify spawner if applicable
      other.gameObject.SendMessage("PickupItem", this);
      // destroy handled on players end
      //Destroy(this.gameObject);
    }
  }

  void NotifySpawner(){
    if (spawner != null){
      spawner.SendMessage("StartCount");
    }
  }
}
