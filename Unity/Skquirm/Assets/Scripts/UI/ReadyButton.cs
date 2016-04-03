using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31.TransitionKit;

public class ReadyButton : MonoBehaviour {
  static Dictionary<int, ReadyButton> Instances = new Dictionary<int, ReadyButton>();
  static Color readyColor = Color.white;
  static Color nonReadyColor = new Color(0.3f, 0.3f, 0.3f, 1f);

  public int playerNum;
  public bool ready;
  private UnityEngine.UI.Image image;

  public TutorialController tutorial;
  public GameObject gui3d;

  private bool started;

	// Use this for initialization
	void Start () {
    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum] = this;
    }else{
      Instances.Add(playerNum, this);
    }

    image = GetComponent<UnityEngine.UI.Image>();
    started = false;
	}

	// Update is called once per frame
	void Update () {
    if (playerNum == 0){
      int readyCount = 0;
      foreach (int key in Instances.Keys){
           ReadyButton button = Instances[key];
           readyCount += button.ready ? 1 : 0;
      }
      if (!started && readyCount >= 4){
        GoToScene.gotoTutorial();
        TransitionKit.onScreenObscured += displayTutorial;
        //GoToScene.startGame("Tank");
        started = true;
      }
    }
	}

  void displayTutorial(){
    tutorial.gameObject.SetActive(true);
    gui3d.SetActive(false);
  }

  public static void UpdateReadyForPlayer(int playerNum, bool readyvalue){
    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum].UpdateReady(readyvalue);
    }
  }

  public static void UpdateButtonForPlayer(int playerNum, float readyvalue){
    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum].UpdateButton(readyvalue);
    }
  }

  void UpdateReady(bool readyvalue){
    ready = readyvalue;
  }

  void UpdateButton(float readyvalue){
    image.color = readyvalue > 0 ? readyColor : nonReadyColor;
  }
}
