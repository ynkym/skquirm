using UnityEngine;
using System.Collections;
using InControl;

public class ReadyController : MonoBehaviour {

  private Rigidbody rb;

  private float horizontal; //for inControl functionality
  private float vertical;

  public int playerNum;
  public Animator charAnimator;

  private int readyHash = Animator.StringToHash("Ready");
  private KeyCode[] keyCodes = {
    KeyCode.Alpha1,
    KeyCode.Alpha2,
    KeyCode.Alpha3,
    KeyCode.Alpha4,
    KeyCode.Alpha5
  };

  InputDevice inputDevice;

	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody>();
    Debug.Log(InputManager.Devices.Count);
	}

	// Update is called once per frame
	void Update () {
    vertical = 0f;
    inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
    if (inputDevice != null)
    {
        horizontal = inputDevice.LeftStickX;
        vertical = inputDevice.Action1;
    }
    if(Input.GetKey(keyCodes[playerNum])){
        vertical = 1f;
    }

    charAnimator.SetFloat(readyHash, vertical); // Go to "Hit" animation
    ReadyButton.UpdateButtonForPlayer(playerNum, vertical);
    if (vertical == 0){
      ReadyButton.UpdateReadyForPlayer(playerNum, false);
    }
	}

  // Fixed time step update, usually for physics
  void FixedUpdate () {
      Vector3 lookDirection = transform.right * (horizontal * 5);
      lookDirection.Normalize();

      Quaternion newRotation;
      if (lookDirection != Vector3.zero){ //Just to avoid the messages on console
          newRotation = Quaternion.LookRotation(lookDirection);
      }else{
          newRotation = new Quaternion(0f, 0f, 0f, 1f); //This is the Quaternion when the direction is the zero vector
      }

      Vector3 temp_rot = newRotation.eulerAngles;
      float time_slerp = 0f;

      if (horizontal != 0){
          time_slerp = 1.9f;
          newRotation.eulerAngles = temp_rot;
          rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRotation, time_slerp * Time.deltaTime);
      }
  }
}
