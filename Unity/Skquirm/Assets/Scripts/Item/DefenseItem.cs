using UnityEngine;
using System.Collections;

public class DefenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public void CombineWith (string anotherType) {
    // TODO
  }

  override public void Activate() {
    //Debug.Log("Used Defense Item");
    DefenseBarrier db = gameObject.AddComponent<DefenseBarrier>() as DefenseBarrier;
    db.setupDefenseBarrier(GlobalSetting.Instance.defenseBarrierDuration,
                           GlobalSetting.Instance.defenseBarrierCount);
  }
}
