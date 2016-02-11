using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

  enum ItemState{
    None,
    Base,
    Combined
  };

  public GameObject itemUI;
  public string type;
  private ItemState state;

  void Start () {
    type = "";
    state = ItemState.None;
  }

  void PickupItem(string newType) {
    if (state == ItemState.None){
      type = newType;
      state = ItemState.Base;
    } else if (state == ItemState.Base){
      //TODO: combine items
      type = type + newType;


      state = ItemState.Combined;
    }
    // ignore new item if already having a combined item

    // notify UI
    itemUI.SendMessage("UpdateUI", this.type);
  }

  public void Activate() {
    if (state != ItemState.None){
      //TODO: actually apply item effects





      // update state
      state = ItemState.None;
      type = "";
      // notify UI
      itemUI.SendMessage("UpdateUI", this.type);
    }
  }
}
