using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour {

	public GlobalSetting globalSet; 

	private Rigidbody rb;
	private Item item;
	public ItemStateUI itemUI;
	public Camera playercamera;
	public float speed;
	public float jumpheight;
	public GameObject shooter; //start position of the projectiles (empty GameObj)
    public int playerNum;
	public int lives = 3;

	public bool testingObj;
	public bool TestWithoutJoystick;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		item = GetComponent<Item>();
	}

	// Update is called once per frame
	void Update () {
        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (inputDevice == null)
        {
			float horizontal; //for inControl functionality
			float vertical;
			float jump;
			float fire;

			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");
			jump = Input.GetAxis ("Jump");
			fire = Input.GetAxis ("Fire3");

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
			//float fire = inputDevice.Action3;
			//float fire = Input.GetAxis ("Fire3"); //temporary

			if (fire > 0 && item != null) {
				item.Activate ();
				Destroy (item);
				item = null;
				itemUI.UpdateUI (item);
			}
        }
        else
        {
            UpdateMovement(inputDevice);
        }
    }

    void UpdateMovement(InputDevice inputdevice){
        float horizontal = inputdevice.LeftStickX; //for inControl functionality
        float vertical = inputdevice.LeftStickY;
        //float horizontal = Input.GetAxis ("Horizontal");
        //float vertical = Input.GetAxis ("Vertical");
        float jump = inputdevice.Action1;
        //float jump = Input.GetAxis ("Jump");

        // only consider vertical input for movement force.
        Vector3 movement = speed * transform.forward * vertical + new Vector3(0, jump * jumpheight, 0);
        rb.AddForce(movement);

        // now count horizontal input to determine the new direction we want to face.
        Vector3 lookDirection = transform.forward * vertical + transform.right * horizontal;
        lookDirection.Normalize();

        if (horizontal != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, 2 * Time.deltaTime);
        }

        // use item
        float fire = inputdevice.Action3;
        //float fire = Input.GetAxis ("Fire3"); //temporary
        if (!testingObj)
        {
            if (fire > 0 && item != null)
            {
                item.Activate();
                Destroy(item);
                item = null;
                itemUI.UpdateUI(item);
            }
        }
    }

	// Fixed time step update, usually for physics
	void FixedUpdate () {
		/*
        var inputDevice = InputManager.ActiveDevice;
		if (!testingObj) {
            
            float horizontal; //for inControl functionality
            float vertical;
			float jump;
			float fire;

			if (!TestWithoutJoystick) {
				horizontal = inputDevice.LeftStickX; //for inControl functionality
				vertical = inputDevice.LeftStickY;
				jump = inputDevice.Action1;
				fire = inputDevice.Action3;
			} else {
				horizontal = Input.GetAxis ("Horizontal");
				vertical = Input.GetAxis ("Vertical");
				jump = Input.GetAxis ("Jump");
				fire = Input.GetAxis ("Fire3");
			}
            
            //float jump = Input.GetAxis ("Jump");

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
            //float fire = inputDevice.Action3;
			//float fire = Input.GetAxis ("Fire3"); //temporary

			if (fire > 0 && item != null) {
				item.Activate ();
				Destroy (item);
				item = null;
				itemUI.UpdateUI (item);
			}
		}*/
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

	public void Damage(){
		//Todo
		print("caused damage to the player");
	}

	public void IncreaseScore(){
		//Todo
	}
}
