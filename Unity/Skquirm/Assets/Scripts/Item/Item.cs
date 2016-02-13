using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  //public GameObject itemUI;

  void Start () {
  }

  virtual public void CombineWith (string anotherType) {
    //TODO: fixme
    //itemUI.SendMessage("UpdateUI", this.type);
  }

  virtual public void Activate() {
    // if (state != ItemState.None){
    //   DefenseBarrier db;
    //   switch (type){
    //     case "Offense":
    //       Debug.Log("Used Offense Item");
    //       // TODO: fire a projectile
    //       break;

    //     case "Defense":
    //       // Debug.Log("Used Defense Item");
    //       db = gameObject.AddComponent<DefenseBarrier>() as DefenseBarrier;
    //       db.setupDefenseBarrier(GlobalSetting.Instance.defenseBarrierDuration,
    //                              GlobalSetting.Instance.defenseBarrierCount);
    //       break;

    //     case "Speed":
    //       // Debug.Log("Used Speed Item");
    //       Rigidbody rb = GetComponent<Rigidbody>();
    //       rb.AddForce(GlobalSetting.Instance.speedItemThrust * transform.forward, ForceMode.Impulse);
    //       break;

    //     case "DefenseDefense":
    //       db = gameObject.AddComponent<DefenseBarrier>() as DefenseBarrier;
    //       // -1 for invincibility regardless of counts
    //       db.setupDefenseBarrier(GlobalSetting.Instance.ddBarrierDuration, -1);
    //       break;

    //     default:
    //       Debug.Log("Used Other Item");
    //       break;
    //   }

    //   // update state
    //   state = ItemState.None;
    //   type = "";
    //   // notify UI
    //   itemUI.SendMessage("UpdateUI", this.type);
    //}
  }
}
