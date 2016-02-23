using UnityEngine;
using System.Collections;

public class SpeedItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    Item newitem = null;
    if (anotherType == "Defense"){
      newitem = gameObject.AddComponent<SpeedDefenseItem>() as SpeedDefenseItem;
    } else if (anotherType == "Offense"){
      newitem = gameObject.AddComponent<OffenseSpeedItem>() as OffenseSpeedItem;
    } else if (anotherType == "Speed"){
      newitem = gameObject.AddComponent<SpeedSpeedItem>() as SpeedSpeedItem;
    }

    if (newitem != null){
       Destroy(this);
       return newitem;
    }
    return this;
  }

  override public Item Activate() {
    // Debug.Log("Used Speed Item");
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.AddForce(GlobalSetting.Instance.speedItemThrust * transform.forward, ForceMode.Impulse);

        return null;
  }
}
