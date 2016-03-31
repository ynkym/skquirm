using UnityEngine;
using System.Collections;

public class SpeedItem : Item {

    public GameObject shooter_back;
    public GameObject boost_explosion;

    public GameObject mine_prefab;
    private GameObject temp_mine;

    PlayerController player_control;

    // Use this for initialization
    void Start () {
        //boost_explosion = Resources.Load("Effects/Flare", (typeof(GameObject))) as GameObject;
        player_control = gameObject.GetComponent<PlayerController>();
        shooter_back = player_control.shooter_back;

        mine_prefab = Resources.Load("Prefabs/Mine", (typeof(GameObject))) as GameObject;
    }

  override public Item CombineWith (string anotherType) {
    // Item newitem = null;
    // if (anotherType == "Defense"){
    //   newitem = gameObject.AddComponent<SpeedDefenseItem>() as SpeedDefenseItem;
    // } else if (anotherType == "Offense"){
    //   newitem = gameObject.AddComponent<OffenseSpeedItem>() as OffenseSpeedItem;
    // } else if (anotherType == "Speed"){
    //   newitem = gameObject.AddComponent<SpeedSpeedItem>() as SpeedSpeedItem;
    // }

    // if (newitem != null){
    //    Destroy(this);
    //    return newitem;
    // }
    return this;
  }

  // activation for "mine" item
  override public Item Activate() {
    //Get the rotation of the car object as EulerAngles
    Vector3 temp_rotation = transform.rotation.eulerAngles;

    //Instantiate
    temp_mine = Instantiate(mine_prefab, shooter_back.transform.position, Quaternion.Euler(temp_rotation)) as GameObject;

    //Set projectile info
    temp_mine.GetComponent<SpeedDefenseProjectile> ().SetInfo (null, player_control);

    temp_mine = null;

    return null;
  }

  // activation for speed boost item
  /*
  override public Item Activate() {
        // Debug.Log("Used Speed Item");
        Rigidbody rb = GetComponent<Rigidbody>();
        GameObject boost;

        //Get the rotation of the car object as EulerAngles
        Vector3 temp_rotation = transform.rotation.eulerAngles;

        rb.AddForce(GlobalSetting.Instance.speedItemThrust * transform.forward, ForceMode.Impulse);

        player_control.IncreaseSpeed(3f);

        //Adjust the rotation of the projectile that will be generated
        temp_rotation.y = -180f;

        boost = Instantiate(boost_explosion, shooter_back.transform.position, Quaternion.identity) as GameObject;

        boost.transform.parent = transform;
        boost.transform.localRotation = Quaternion.Euler(temp_rotation);

        Destroy(boost, 3f);
        return null;
  }*/
}
