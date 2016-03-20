using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Sounds")]
    public AudioSource bumpAudioSource;
    public AudioSource riderAudioSource;
    public AudioSource riderEffects;
    public AudioSource reactionAudioSource;
    public AudioClip p2pBump;
    public AudioClip p2wallBump;
    public AudioClip[] damageAudios;
    public AudioClip[] comemorationAudios;
    public AudioClip boostSound;

    [Header("Sewer Stage Variables")]
    public Transform pipe1;
    public Transform pipe2;
    public Transform pipe3;
    public Transform pipe4;
    public Transform pipe5;
    public Transform pipe6;
    public Transform pipe7;
    public Transform pipe8;

    

    InputDevice inputDevice;
    bool button_pressed = false;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
		item = GetComponent<Item>();
        health = GetComponent<Health>();

        PlayerScore.Create(playerNum);

        rider_particles = GetComponent<RiderParticles>();
        
        GlobalSetting.Instance.registerPlayer(this.gameObject);
        this.enabled = false; // wait until game start
	}

	// Update is called once per frame
	void Update () {
        inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (inputDevice != null && !TestWithoutJoystick)
        {
            horizontal = inputDevice.LeftStickX; //for inControl functionality
            //vertical = inputdevice.LeftStickY;
            vertical = inputDevice.Action1;
            jump = inputDevice.Action2;
            fire = inputDevice.Action3;

            print(inputDevice.AnyButton);
            if (inputDevice.AnyButton == null) button_pressed = false;
            else button_pressed = true;
        }
        else {
            if (playerNum == 0)
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
                jump = Input.GetAxis("Jump");
                fire = Input.GetButtonDown("Fire3");

                if (Input.anyKeyDown) button_pressed = true;
                else button_pressed = false;
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

        Quaternion newRotation;

        if (lookDirection != Vector3.zero) //Just to avoid the messages on console
        {
            newRotation = Quaternion.LookRotation(lookDirection);
        }
        else
        {
            newRotation = new Quaternion(0f, 0f, 0f, 1f); //This is the Quaternion when the direction is the zero vector
        }

        Vector3 temp_rot = newRotation.eulerAngles;
        float time_slerp = 0f;

        riderAudioSource.volume = Mathf.Sqrt(Mathf.Pow(vertical, 2f) + Mathf.Pow(horizontal, 2f))/2.8f;

        if (horizontal != 0)
        {
            if (vertical != 0)
            {
                time_slerp = 1.9f;
                temp_rot.z = -8f * horizontal;
                temp_rot.x = 0f;
            }
            else {
                time_slerp = 0.7f;
                temp_rot.z = -8f * horizontal;
                temp_rot.x = 0f;
            }
            newRotation.eulerAngles = temp_rot;
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, time_slerp * Time.deltaTime);
        }
        else {
            if (!button_pressed)
            {
                time_slerp = 3f;
                temp_rot = transform.rotation.eulerAngles;
                temp_rot.z = 0f;
                temp_rot.x = 0f;

                newRotation.eulerAngles = temp_rot;
                rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, time_slerp * Time.deltaTime);
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
            //Reaction Sounds
            reactionAudioSource.clip = damageAudios[Random.Range(0, 2)];
            reactionAudioSource.Play();

            return true;
        } else {
            // block by barrier
            barrier.breakingBarrier();
            return false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            DefenseBarrier dBarrier = GetComponent<DefenseBarrier>();
            OffenseDefenseBarrier odBarrier = GetComponent<OffenseDefenseBarrier>();
            bumpAudioSource.clip = p2pBump;
            bumpAudioSource.Play();

            if (odBarrier != null)
            {
                // this player have OD barrier

                collision.gameObject.GetComponent<PlayerController>().TryToHurt();
                odBarrier.breakingBarrier();
                test++;
                Debug.Log(test + " " + gameObject.name);
                test = 0;
            }
            else if (dBarrier != null)
            {
                dBarrier.BubbleReaction();
            }
        }
        else if (collision.gameObject.tag == "obstacle")
        {
            bumpAudioSource.clip = p2wallBump;
            bumpAudioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "SewerScene")
        {
            //it doesnt teleport with every collision now, however it doesnt always teleport for some odd reason.
            if (other.gameObject.CompareTag("pipe1"))
            {
                //transfers to pipe6 at coordinates X:22, Y:14, Z:18
                transform.position = new Vector3(35, 18, 28);
                transform.LookAt(pipe5);
            }
            else if (other.gameObject.CompareTag("pipe2"))
            {
                //transfer to pipe 5 at coordinates X:20.5, Y:14, Z:-15
                transform.position = new Vector3(-17, 18, -19);
                transform.LookAt(pipe6);
            }
            else if (other.gameObject.CompareTag("pipe3"))
            {
                //transfer to pipe 7 at coordinates X:-16, Y:14, Z:18
                transform.position = new Vector3(-17, 18, 29);
                transform.LookAt(pipe8);
            }
            else if (other.gameObject.CompareTag("pipe4"))
            {
                //transfer to pipe 8 at coordinates X:20.5, Y:14, Z:-15
                transform.position = new Vector3(36, 18, -17.5f);
                transform.LookAt(pipe7);
            }
            else if (other.gameObject.CompareTag("pipe5"))
            {
                //transfer to pipe 2 at coordinates X:3, Y:4, Z:26
                transform.position = new Vector3(9, 4, 40);
                transform.LookAt(pipe1);
            }
            else if (other.gameObject.CompareTag("pipe6"))
            {
                //transfer to pipe 1 at coordinates X:2.5, Y:4, Z:-25
                transform.position = new Vector3(7, 4, -32);
                transform.LookAt(pipe2);
            }
            else if (other.gameObject.CompareTag("pipe7"))
            {
                //transfer to pipe 3 at coordinates X:28, Y:4, Z:1.5
                transform.position = new Vector3(43.5f, 4, 7);
                transform.LookAt(pipe4);
            }
            else if (other.gameObject.CompareTag("pipe8"))
            {
                //transfer to pipe 4 at coordinates X:-23, Y:4, Z:2
                transform.position = new Vector3(-28, 4, 6);
                transform.LookAt(pipe3);
            }
        }
    }

    public void IncreaseScore(){
        PlayerScore.AddScoreToPlayer(playerNum, 1); //add 1 to the score
        reactionAudioSource.clip = comemorationAudios[Random.Range(0, 2)];
        reactionAudioSource.Play();
    }

    public void IncreaseSpeed(float time) {
        StartCoroutine(IncreaseTheSpeedFor(time));
    }

    IEnumerator IncreaseTheSpeedFor(float time) {
        riderEffects.clip = boostSound;
        riderEffects.Play();

        speed = 70f;
        yield return new WaitForSeconds(time);
        speed = 40f;
    }

    public void CarSound() {
        riderAudioSource.Play();
    }
}
