using UnityEngine;
using System.Collections;

public class DefenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    if (anotherType == "Defense"){
      Item newitem = gameObject.AddComponent<DefenseDefenseItem>() as DefenseDefenseItem;
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
