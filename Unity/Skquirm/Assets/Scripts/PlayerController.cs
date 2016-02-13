using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  private Rigidbody rb;
  private Item item;
  public ItemStateUI itemUI;
  public Camera playercamera;
  public float speed;
  public float jumpheight;

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
    item = GetComponent<Item>();
  }

  // Update is called once per frame
  void Update () {

  }

  // Fixed time step update, usually for physics
  void FixedUpdate () {

    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    float jump = Input.GetAxis("Jump");

    // only consider vertical input for movement force.
    Vector3 movement = speed * transform.forward*vertical + new Vector3(0, jump * jumpheight, 0);
    rb.AddForce(movement);

    // now count horizontal input to determine the new direction we want to face.
    Vector3 lookDirection = transform.forward*vertical + transform.right*horizontal;
    lookDirection.Normalize();

    if (horizontal != 0){
      Quaternion newRotation = Quaternion.LookRotation(lookDirection);
      rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, 2 * Time.deltaTime);
    }

    // use item
    float fire = Input.GetAxis("Fire3"); //temporary
    if (fire > 0 && item != null){
      item.Activate();
      Destroy(item);
      item = null;
      itemUI.UpdateUI(item);
    }
  }

  void PickupItem (string newType) {
    if (item != null){
      item = item.CombineWith(newType);
    }else{
      // just to get it working... will look for more elegant ways later...
      if (newType == "Defense"){
        item = gameObject.AddComponent<DefenseItem>() as DefenseItem;
      }else if (newType == "Offense"){
        item = gameObject.AddComponent<OffenseItem>() as OffenseItem;
      }else if (newType == "Speed"){
        item = gameObject.AddComponent<SpeedItem>() as SpeedItem;
      }
    }
    itemUI.UpdateUI(item);
  }
}
