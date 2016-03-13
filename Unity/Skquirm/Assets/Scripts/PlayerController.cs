using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private Item item;
	public Camera playercamera;
	public float speed;
	public float jumpheight;

	public GameObject shooter; //start position of the projectiles (empty GameObj)
	public GameObject shooter_back; //drop position of the mine (also an empty Obj for coordinate purposes)

    public int playerNum;

    public Animator charAnimator;

	public bool testingObj;
	public bool TestWithoutJoystick;
    private Health health;

    private float horizontal; //for inControl functionality
    private float vertical;
    private float jump;
    private bool fire;

    private int test = 0;

    // hash variables for animation
    private int hitHash = Animator.StringToHash("Hit");

    private RiderParticles rider_particles;

    [SerializeField] private float rotationAngle;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
		item = GetComponent<Item>();
        health = GetComponent<Health>();

        PlayerScore.Create(playerNum);
        GlobalSetting.Instance.registerPlayer(this.gameObject);

        rider_particles = GetComponent<RiderParticles>();
	}

	// Update is called once per frame
	void Update () {
        InputDevice inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (inputDevice != null && !TestWithoutJoystick)
        {
            horizontal = inputDevice.LeftStickX; //for inControl functionality
            //vertical = inputdevice.LeftStickY;
            vertical = inputDevice.Action1;
            jump = inputDevice.Action2;
            fire = inputDevice.Action3;
        }
        else {
            if (playerNum == 0)
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
                jump = Input.GetAxis("Jump");
                fire = Input.GetButtonDown("Fire3");
            }
        }

        if (fire && item != null)
        {
            Item newItem = item.Activate();
            Destroy(item);
            item = newItem;
            ItemStateUI.UpdateForPlayer(playerNum, item);
        }
        rider_particles.UpdateParticles(horizontal, vertical);
    }


    // Fixed time step update, usually for physics, everything moved to updateMovement
    void FixedUpdate () {
        Vector3 movement = speed * transform.forward * vertical + new Vector3(0, jump * jumpheight, 0);
        rb.AddForce(movement);

        // now count horizontal input to determine the new direction we want to face.
        Vector3 lookDirection = transform.forward * vertical + transform.right * horizontal;
        lookDirection.Normalize();

        Quaternion newRotation = Quaternion.LookRotation(lookDirection);
        Vector3 temp_rot = newRotation.eulerAngles;
        float time_slerp = 0f;

        if (horizontal != 0)
        {
            if (vertical != 0)
            {
                time_slerp = 1.9f;

                if (playerNum == 0)
                    print(temp_rot);

                temp_rot.z = -10f * horizontal;
                temp_rot.x = 0f;
            }
            else {
                time_slerp = 0.7f;
                temp_rot.z = -10f * horizontal;
                temp_rot.x = 0f;
            }
            newRotation.eulerAngles = temp_rot;
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, time_slerp * Time.deltaTime);
        }
        else {
            time_slerp = 3f;
            temp_rot = transform.rotation.eulerAngles;
            temp_rot.z = 0f;
            temp_rot.x = 0f;

            newRotation.eulerAngles = temp_rot;
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, time_slerp * Time.deltaTime);
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
			ItemStateUI.UpdateForPlayer (playerNum, item);
		}
	}

    public bool TryToHurt(){
        //Todo
        DefenseBarrier barrier = GetComponent<DefenseBarrier> ();
        if (barrier == null) {
            // Do actual damage
            health.getDamaged(playerNum);
            StartCoroutine(health.getDamaged(playerNum));
            // quick hack: shift the position vertically to make bouncing due to buoyancy happen
            rb.transform.position = rb.transform.position + new Vector3(0, -1, 0);
            // set a trigger to animate the character
            charAnimator.SetTrigger(hitHash); // Go to "Hit" animation

            print("caused damage to the player");
            return true;
        } else {
            // block by barrier
            barrier.breakingBarrier();
            return false;
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Player") {
            OffenseDefenseBarrier odBarrier = GetComponent<OffenseDefenseBarrier> ();

            if (odBarrier != null) {
                // this player have OD barrier

                collision.gameObject.GetComponent<PlayerController>().TryToHurt();
                odBarrier.breakingBarrier();
                test++;
                Debug.Log(test + " " + gameObject.name);
                test = 0;
            }

        }
    }


    public void IncreaseScore(){
        PlayerScore.AddScoreToPlayer(playerNum, 1); //add 1 to the score
    }
}
