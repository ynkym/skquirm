using UnityEngine;
using System.Collections;

public class OffenseDefenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    // ignore
    return this;
  }

  override public void Activate () {
    Debug.Log("Used OD Item");
	OffenseDefenseBarrier odb = gameObject.AddComponent<OffenseDefenseBarrier>() as OffenseDefenseBarrier;
	odb.setupDefenseBarrier(GlobalSetting.Instance.defenseBarrierDuration,
		                       GlobalSetting.Instance.defenseBarrierCount);

  }
}
