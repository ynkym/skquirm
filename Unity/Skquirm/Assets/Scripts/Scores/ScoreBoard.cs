using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

  public Text titleText;
  public Text pressText;
  public GameObject[] winnerLogo;

  public PlayerScoreUI[] scoreUIs;
  public Sprite[] avatars;
  public Sprite[] nextButtons;

  public Material[] priMaterials;
  public Material[] secMaterials;

  public SkinnedMeshRenderer winner;
  private int winnerNum;
  private Color color = Color.white;

	// Use this for initialization
	void Start () {
    int currentGame = GoToScene.currentGame;
    //currentGame = 1;
    if (currentGame > 0 && currentGame < GoToScene.maxGame){
      titleText.text = "Result: Stage " + currentGame;
    }else{
      titleText.gameObject.SetActive(false);
      float w = 0;
      foreach(GameObject letter in winnerLogo){
        letter.SetActive(true);
        StartCoroutine("doAnimation", new object[2]{letter, 0.3f * w});
        w++;
      }
    }

    List<PlayerScore> scores = PlayerScore.GetSortedScores();
    int i = 0;
    winnerNum = scores[0].playerNum;
    winner.materials = new Material[2]{ priMaterials[winnerNum], secMaterials[winnerNum] };
    foreach (PlayerScore score in scores){
      scoreUIs[i].SetPlayerNum(score.playerNum);
      scoreUIs[i].UpdateScore(score.score, avatars[score.playerNum], nextButtons[score.playerNum]);
      i++;
    }
	}

  void Update () {
    float fade = 0.5f * (Mathf.Sin(Time.time * 4) + 1);
    color.a = fade;
    pressText.color = color;
  }

  private IEnumerator doAnimation(object[] param){
    GameObject letter = (GameObject)param[0];
    float wait = (float)param[1];
    yield return new WaitForSeconds(wait);
    iTween.MoveTo(letter, iTween.Hash("y", -3f, "time", 1.0f, "islocal", true, "EaseType", iTween.EaseType.easeInOutSine, "loopType", iTween.LoopType.pingPong));
  }
}
