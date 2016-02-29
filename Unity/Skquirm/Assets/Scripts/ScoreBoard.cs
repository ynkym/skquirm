using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

  public Text[] scoreText;

	// Use this for initialization
	void Start () {
	   ArrayList players = PlayerController.getAllPlayers();

    //Debug.Log(players.Count);
    for (int i = 0; i < players.Count; i++){
      PlayerController controller = (PlayerController) players[i];
      int playerId = controller.playerNum;
      scoreText[playerId].text = "" + controller.GetScore();
    }
	}

	// Update is called once per frame
	void Update () {

	}
}
