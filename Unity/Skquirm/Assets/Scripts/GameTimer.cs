using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

  public int stageNum;

  public float remainingTime = 3 * 60; // 3 mins by default
  public GameObject timerUIObject;
  public float gameStartCount = 3 * 60 + 5;
  private bool gameOver;
  private bool gameStarted;
  private Text timerText;
  private Color tcol;
  private Color textColor;
  private float fadeoutTimer;
  private int index;

  private MaskableGraphic fadeGraphic;

  private List<PlayerController> controllers;

  private TimeNotification[] notification;
  public Image[] timerImage;

	// Use this for initialization
	void Start () {
    gameOver = false;
    gameStarted = false;

    timerText = timerUIObject.GetComponent<Text>();
    textColor = timerText.color;
    tcol = Color.white;
    tcol.a = 0;
		fadeoutTimer = 0;
		index = 0;

    notification = new TimeNotification[]{
      new TimeNotification( gameStartCount + 3f, 0.5f, "3", 0 ),
      new TimeNotification( gameStartCount + 2f, 0.5f, "2", 1 ),
      new TimeNotification( gameStartCount + 1f, 0.5f, "1", 2 ),
      new TimeNotification( gameStartCount, 1.0f, "Go!", 3 ),
      new TimeNotification( 20f, 0.5f, "0:20" ),
      new TimeNotification( 10f, 0.5f, "0:10" ),
      new TimeNotification( 5f, 0.5f, "0:05" ),
      new TimeNotification( 0f, 1.0f, "Time Up!", 4 ),
    };
  }

    public AudioSource timer_sound;


  void ThereIsWinner () {
    gameOver = true;
    index = notification.Length;
    fadeoutTimer = 1.0f;
    timerText.text = "Game Set!";
    tcol.a = 1;
    remainingTime = 0.0f;
    GameOver();
  }

  void GameOver () {
    gameOver = true;
    // stop all players
    foreach(PlayerController ctl in controllers){
      if (ctl != null)
        ctl.enabled = false;
    }
  }

  void GameStart () {
    gameStarted = true;
    controllers = new List<PlayerController>();
    ArrayList players = GetComponent<GlobalSetting>().getAllPlayers();
    for (int i = 0; i < players.Count; i++){
      GameObject currPlayer = (GameObject)players[i];
      controllers.Add(currPlayer.GetComponent<PlayerController>());
    }
    foreach(PlayerController ctl in controllers){
        ctl.enabled = true;
            ctl.CarSound();
    }
  }

	// Update is called once per frame
	void Update () {
    remainingTime = remainingTime - Time.deltaTime;
		if (index < notification.Length && remainingTime < notification[index].time) {
      timer_sound.Play();
      if (notification[index].imageIndex >= 0 && notification[index].imageIndex < timerImage.Length){
        Image image = timerImage[notification[index].imageIndex];
        timerUIObject = image.gameObject;
        fadeGraphic = image;
        tcol = Color.white;
      }else{
        timerUIObject = timerText.gameObject;
        fadeGraphic = timerText;
        tcol = textColor;
        timerText.text = notification[index].text;
      }
      tcol.a = 1;
      fadeoutTimer = notification[index].fadeTime;
      timerUIObject.transform.localScale = new Vector3(1,1,1);
      fadeGraphic.color = tcol;
      index++;
		}

		if (fadeoutTimer > 0) {
			float scaleFactor = (Time.deltaTime / fadeoutTimer);
			tcol.a = tcol.a - (scaleFactor);
			timerUIObject.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
			fadeGraphic.color = tcol;
			if (tcol.a <= 0) {
				tcol.a = 0;
				fadeoutTimer = 0;
        timerUIObject.transform.localScale = new Vector3(1,1,1);
		  }
      fadeGraphic.color = tcol;
		}

        if (remainingTime <= gameStartCount && gameStarted == false){
            GameStart();
        }

        if (remainingTime < 0 && gameOver == false){
            GameOver();
        }

        // when enough time has passed, switch scene
        if (remainingTime < -3){
            GoToScene.goToResult(stageNum);
            //GoToScene.sceneTransition("GameOver");
            Destroy(this);
        }

        CustomizedSettingsForScene();
    }

    public virtual void CustomizedSettingsForScene() {} //Used By children classes
}

public struct TimeNotification {
  public float time;
  public float fadeTime;
  public string text;
  public int imageIndex;
  public TimeNotification (float t, float ft, string x, int i = -1){
    time = t; fadeTime = ft; text = x; imageIndex = i;
  }
}
