using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class PlayerScoreUI : MonoBehaviour {

  public Text scoreText;
  public Image avatar;
  public Image readyButton;

  public int playerNum;
  private float next;
  InputDevice inputDevice;
  private bool readyForNext;

  private static int readyPlayer;

  private static KeyCode[] keyCodes = {
    KeyCode.Alpha1,
    KeyCode.Alpha2,
    KeyCode.Alpha3,
    KeyCode.Alpha4,
    KeyCode.Alpha5
  };

	// Use this for initialization
	public void UpdateScore(int score, Sprite av, Sprite re){
    avatar.sprite = av;
    readyButton.sprite = re;
    scoreText.text = "" + score;
    readyPlayer = 0;
  }

  public void SetPlayerNum(int num){
    this.playerNum = num;
  }

  void Start () {
    readyForNext = false;
  }

  void Update () {
    float next = 0f;
    if (playerNum >= 0){
      inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
      if (inputDevice != null){
        next = inputDevice.Action1;
      }
      if(Input.GetKey(keyCodes[playerNum])){
        next = 1f;
      }
    }

    if (!readyForNext && next > 0){
      readyForNext = true;
      iTween.RotateTo(readyButton.gameObject, iTween.Hash("y", 0, "time", 0.5f, "oncomplete", "CheckReady", "oncompletetarget", gameObject));
    }
  }

  public void CheckReady () {
    readyPlayer += 1;
    Debug.Log(readyPlayer + " players are ready");
    if (readyPlayer >= 4){
      GoToScene.goNext();
    }
  }
}
