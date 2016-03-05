using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  //public GameObject itemUI;

  virtual public Item CombineWith (string anotherType){
    return this;
  }

  virtual public Item Activate () {
        return null;
  }
}
