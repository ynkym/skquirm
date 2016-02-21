using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GlobalSetting globalSet; 

	private Rigidbody rb;
	private Item item;
	public ItemStateUI itemUI;
	public Camera playercamera;
	public float speed;
	public float jumpheight;
	public GameObject shooter; //start position of the projectiles (empty GameObj)

	public int lives = 3;

	public bool testingObj;

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
		if (!testingObj) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");

			float jump = Input.GetAxis ("Jump");

			// only consider vertical input for movement force.
			Vector3 movement = speed * transform.forward * vertical + new Vector3 (0, jump * jumpheight, 0);
			rb.AddForce (movement);

			// now count horizontal input to determine the new direction we want to face.
			Vector3 lookDirection = transform.forward * vertical + transform.right * horizontal;
			lookDirection.Normalize ();

			if (horizontal != 0) {
				Quaternion newRotation = Quaternion.LookRotation (lookDirection);
				rb.transform.rotation = Quaternion.Slerp (rb.transform.rotation, newRotation, 2 * Time.deltaTime);
			}

			// use item
			float fire = Input.GetAxis ("Fire3"); //temporary
			if (fire > 0 && item != null) {
				item.Activate ();
				Destroy (item);
				item = null;
				itemUI.UpdateUI (item);
			}
		}
	}

	void PickupItem (string newType) {
		if (!testingObj) {
			if (item != null) {
				item = item.CombineWith (newType);
			} else {
				// just to get it working... will look for more elegant ways later...
				if (newType == "Defense") {
					item = gameObject.AddComponent<DefenseItem> () as DefenseItem;
				} else if (newType == "Offense") {
					item = gameObject.AddComponent<OffenseItem> () as OffenseItem;
				} else if (newType == "Speed") {
					item = gameObject.AddComponent<SpeedItem> () as SpeedItem;
				}
			}
			itemUI.UpdateUI (item);
		}
	}

	public void TryToHurt(){
		//Todo
		DefenseBarrier barrier = GetComponent<DefenseBarrier> ();
		if (barrier == null) {
			// Do actual damage
			print("caused damage to the player");
		} else {
			// block by barrier
			barrier.breakingBarrier();
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player") {
			OffenseDefenseBarrier odBarrier = GetComponent<OffenseDefenseBarrier> ();
			if (odBarrier != null) {
				// this player have OD barrier
				collision.gameObject.GetComponent<PlayerController>().TryToHurt();
				odBarrier.breakingBarrier();
			}
		}
	}

	public void IncreaseScore(){
		//Todo
	}
}
