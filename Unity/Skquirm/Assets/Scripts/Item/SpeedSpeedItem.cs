using UnityEngine;
using System.Collections;

public class SpeedSpeedItem : Item {

	// Use this for initialization
	void Start () {

	}

  override public Item CombineWith (string anotherType) {
    // ignore
    return this;
  }

  override public Item Activate () {
    Debug.Log("Used SS Item");
        // TODO: implement
        SpeedSpeedBuff ssb = gameObject.AddComponent<SpeedSpeedBuff>();
        
        // get a random item excluding itself
        int rand = Random.Range(0, GlobalSetting.getTotalNumOfCombinedItem()-1);
        Item randCombinedItem = null;
        switch(rand)
        {
            case 0:
                randCombinedItem = gameObject.AddComponent<OffenseOffenseItem>();
                break;
            case 1:
                randCombinedItem = gameObject.AddComponent<OffenseDefenseItem>();
                break;
            case 2:
                //randCombinedItem = gameObject.AddComponent<OffenseSpeedItem>();
                //break;
            case 3:
                randCombinedItem = gameObject.AddComponent<DefenseDefenseItem>();
                break;
            case 4:
                //randCombinedItem = gameObject.AddComponent<SpeedDefenseItem>() as SpeedDefenseItem;
                //break;
            default:
                break;
        }
        return randCombinedItem;
  }
}
