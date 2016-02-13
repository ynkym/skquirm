using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  //public GameObject itemUI;

  void Start () {
  }

  virtual public Item CombineWith (string anotherType){
    return this;
  }

  virtual public void Activate () {
  }
}
