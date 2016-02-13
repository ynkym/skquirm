using UnityEngine;
using System.Collections;

public class OffenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    Item newitem = null;
    if (anotherType == "Defense"){
      newitem = gameObject.AddComponent<OffenseDefenseItem>() as OffenseDefenseItem;
    } else if (anotherType == "Offense"){
      newitem = gameObject.AddComponent<OffenseOffenseItem>() as OffenseOffenseItem;
    } else if (anotherType == "Speed"){
      newitem = gameObject.AddComponent<OffenseSpeedItem>() as OffenseSpeedItem;
    }

    if (newitem != null){
       Destroy(this);
       return newitem;
    }
    return this;
  }

  override public void Activate() {
    Debug.Log("Used Offense Item");
    // TODO: implement
  }
}
