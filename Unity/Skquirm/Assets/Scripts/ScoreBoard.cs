using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

  public Text[] scoreText;

	// Use this for initialization
	void Start () {
    //Debug.Log(GlobalSetting.Instance);
        ArrayList players = GlobalSetting.Instance.getAllPlayers();

    //Debug.Log(players.Count);
    for (int i = 0; i < players.Count; i++){
      GameObject player = (GameObject)players[i];

      PlayerController controller = player.GetComponent<PlayerController>();
      int playerId = controller.playerNum;
      scoreText[playerId].text = "" + controller.GetScore();
    }
	}

	// Update is called once per frame
	void Update () {

	}
}
