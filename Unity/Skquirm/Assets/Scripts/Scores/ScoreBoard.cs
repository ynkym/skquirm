using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

  public Text titleText;
  public Text buttonText;

  public Text[] scoreText;

	// Use this for initialization
	void Start () {
    int currentGame = GoToScene.currentGame;
    Debug.Log(currentGame);
    if (currentGame > 0 && currentGame < GoToScene.maxGame){
      titleText.text = "Result: Stage " + GoToScene.currentGame;
      buttonText.text = "Go Next";
    }


    //Debug.Log(GlobalSetting.Instance);
    ArrayList players = GlobalSetting.Instance.getAllPlayers();

    //Debug.Log(players.Count);
    for (int i = 0; i < players.Count; i++){
      // GameObject player = (GameObject)players[i];

      // PlayerController controller = player.GetComponent<PlayerController>();
      // int playerId = controller.playerNum;
      scoreText[i].text = "" + PlayerScore.GetScore(i);
    }
	}

	// Update is called once per frame
	void Update () {

	}
}
