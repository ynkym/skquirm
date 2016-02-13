using UnityEngine;
using System.Collections;

public class DefenseDefenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    // ignore
    return this;
  }

  override public void Activate () {
    Debug.Log("Used DD Item");
    DefenseBarrier db = gameObject.AddComponent<DefenseBarrier>() as DefenseBarrier;
    // -1 for invincibility regardless of counts
    db.setupDefenseBarrier(GlobalSetting.Instance.ddBarrierDuration, -1);
  }
}
