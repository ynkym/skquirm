using UnityEngine;
using System.Collections;

public class DefenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    Item newitem = null;
    if (anotherType == "Defense"){
      newitem = gameObject.AddComponent<DefenseDefenseItem>() as DefenseDefenseItem;
    } else if (anotherType == "Offense"){
      newitem = gameObject.AddComponent<OffenseDefenseItem>() as OffenseDefenseItem;
    } else if (anotherType == "Speed"){
      newitem = gameObject.AddComponent<SpeedDefenseItem>() as SpeedDefenseItem;
    }

    if (newitem != null){
       Destroy(this);
       return newitem;
    }
    return this;
  }

  override public void Activate() {
    //Debug.Log("Used Defense Item");
    DefenseBarrier db = gameObject.AddComponent<DefenseBarrier>() as DefenseBarrier;
    db.setupDefenseBarrier(GlobalSetting.Instance.defenseBarrierDuration,
                           GlobalSetting.Instance.defenseBarrierCount);
  }
}
