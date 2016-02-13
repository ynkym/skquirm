using UnityEngine;
using System.Collections;

public class OffenseOffenseItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    // ignore
    return this;
  }

  override public void Activate () {
    Debug.Log("Used OO Item");
    // TODO: implement
  }
}
