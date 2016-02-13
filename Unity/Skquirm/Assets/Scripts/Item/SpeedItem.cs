using UnityEngine;
using System.Collections;

public class SpeedItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    // TODO
    return this;
  }

  override public void Activate() {
    // Debug.Log("Used Speed Item");
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.AddForce(GlobalSetting.Instance.speedItemThrust * transform.forward, ForceMode.Impulse);
  }
}
