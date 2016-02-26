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

	// Use this for initialization
	void Start () {
    gameOver = false;

    timerText = timerUIObject.GetComponent<Text>();
    tcol = timerText.color;
    tcol.a = 0;
    timerText.color = tcol;
	}

	// Update is called once per frame
	void Update () {
    remainingTime = remainingTime - Time.deltaTime;
    if (remainingTime < 0){
      if (gameOver == false){
        gameOver = true;
        // time is up, display message
        tcol.a = 1;

        // TODO: also stop all players

      }

      float scaleFactor = (0.5f * Time.deltaTime);

      // fade out the text
      tcol.a = tcol.a - (0.5f * Time.deltaTime);
      timerUIObject.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
      timerText.color = tcol;
      // when enough time has passed, switch scene
      if (remainingTime < -2){
        SceneManager.LoadScene("GameOver");
      }
    }
	}
}
