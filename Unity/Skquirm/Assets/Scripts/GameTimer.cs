using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

  public float remainingTime = 3 * 60; // 3 mins by default
  public GameObject timerUIObject;
  private bool gameOver;
  private Text timerText;
  private Color tcol;
  private float fadeoutTimer;
  private int index;

  private TimeNotification[] notification = new TimeNotification[]{
    new TimeNotification( 20f, 0.5f, "0:20" ),
    new TimeNotification( 10f, 0.5f, "0:10" ),
    new TimeNotification( 5f, 0.5f, "0:05" ),
    new TimeNotification( 0f, 1.0f, "Time Up!" ),
  };

	// Use this for initialization
	void Start () {
    gameOver = false;

    timerText = timerUIObject.GetComponent<Text>();
    tcol = timerText.color;
    tcol.a = 0;
    timerText.color = tcol;
		fadeoutTimer = 0;
		index = 0;
	}

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
    ArrayList players = GetComponent<GlobalSetting>().getAllPlayers();
    for (int i = 0; i < players.Count; i++){
            GameObject currPlayer = (GameObject)players[i];
            PlayerController controller = currPlayer.GetComponent<PlayerController>();
      controller.enabled = !controller.enabled;
    }
  }

	// Update is called once per frame
	void Update () {
    remainingTime = remainingTime - Time.deltaTime;
		if (index < notification.Length && remainingTime < notification[index].time) {
			tcol.a = 1;
			fadeoutTimer = notification[index].fadeTime;
      timerText.text = notification[index].text;
      index++;
		}

		if (fadeoutTimer > 0) {
			float scaleFactor = (Time.deltaTime / fadeoutTimer);
			tcol.a = tcol.a - scaleFactor;
			timerUIObject.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
			timerText.color = tcol;
			if (tcol.a <= 0) {
				tcol.a = 0;
				fadeoutTimer = 0;
        timerUIObject.transform.localScale = new Vector3(1,1,1);
			}
      timerText.color = tcol;
		}

    if (remainingTime < 0 && gameOver == false){
      GameOver();
    }

    // when enough time has passed, switch scene
    if (remainingTime < -3){
      SceneManager.LoadScene("GameOver");
    }
	}
}

public struct TimeNotification {
  public float time;
  public float fadeTime;
  public string text;
  public TimeNotification (float t, float ft, string x){
    time = t; fadeTime = ft; text = x;
  }
}
