using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  //public GameObject itemUI;

  void Start () {
  }

  virtual public Item CombineWith (string anotherType){
    //TODO: fixme
    //itemUI.SendMessage("UpdateUI", this.type);
    return this;
  }

  virtual public void Activate () {
  }
}
