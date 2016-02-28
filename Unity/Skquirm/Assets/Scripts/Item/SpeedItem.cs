using UnityEngine;
using System.Collections;

public class SpeedItem : Item {

    public GameObject shooter_back;
    public GameObject boost_explosion;

    // Use this for initialization
    void Start () {
        boost_explosion = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Standard Assets/ParticleSystems/Prefabs/Flare.prefab", (typeof(GameObject))) as GameObject;
        shooter_back = gameObject.GetComponent<PlayerController>().shooter_back;
    }

  override public Item CombineWith (string anotherType) {
    Item newitem = null;
    if (anotherType == "Defense"){
      newitem = gameObject.AddComponent<SpeedDefenseItem>() as SpeedDefenseItem;
    } else if (anotherType == "Offense"){
      newitem = gameObject.AddComponent<OffenseSpeedItem>() as OffenseSpeedItem;
    } else if (anotherType == "Speed"){
      newitem = gameObject.AddComponent<SpeedSpeedItem>() as SpeedSpeedItem;
    }

    if (newitem != null){
       Destroy(this);
       return newitem;
    }
    return this;
  }

  override public Item Activate() {
        // Debug.Log("Used Speed Item");
        Rigidbody rb = GetComponent<Rigidbody>();
        GameObject boost;

        //Get the rotation of the car object as EulerAngles
        Vector3 temp_rotation = transform.rotation.eulerAngles;

        rb.AddForce(GlobalSetting.Instance.speedItemThrust * transform.forward, ForceMode.Impulse);

        //Adjust the rotation of the projectile that will be generated
        temp_rotation.y = -180f;

        boost = Instantiate(boost_explosion, shooter_back.transform.position, Quaternion.identity) as GameObject;

        boost.transform.parent = transform;
        boost.transform.localRotation = Quaternion.Euler(temp_rotation);

        Destroy(boost, 2f);
        return null;
  }
}
