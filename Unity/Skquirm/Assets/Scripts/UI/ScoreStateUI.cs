using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreStateUI : MonoBehaviour {

  static float[] playerScaleY = { 1, 1, -1, -1 };
  static float[] playerScaleX = { 1, -1, 1, -1 };

  static Dictionary<int, ScoreStateUI> Instances = new Dictionary<int, ScoreStateUI>();
  public static void UpdateForPlayer(int playerNum){
    //Debug.Log(playerNum);
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].UpdateUI();
    }
  }
  public static void Clear(){ Instances.Clear(); }

  public enum PlayerColor{ yellow, red, green, purple }
  public PlayerColor color;

  public int playerNum;
  public UnityEngine.UI.Image ringImage;
  public UnityEngine.UI.Image baseImage;
  public UnityEngine.UI.Text scoreText;

  private GameObject popObject;
  private ImageAnimation[] lives;
  private int prevLife;

  void Start () {
      // load assets
      string colorstring = color.ToString();
      Sprite bubbleSprite = (Resources.Load("Sprites/Life/circle_single_" + colorstring, (typeof(Sprite))) as Sprite);
      Sprite ringSprite = (Resources.Load("Sprites/Life/ring_" + colorstring, (typeof(Sprite))) as Sprite);

      // setup the whole UI
      gameObject.transform.localScale = new Vector3(playerScaleX[playerNum], playerScaleY[playerNum], 1);
      ringImage.sprite = ringSprite;
      baseImage.sprite = bubbleSprite;

      scoreText.text = "" + PlayerScore.GetScore(playerNum);

    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum] = this;
    }else{
      Instances.Add(playerNum, this);
    }
  }

  public void UpdateUI (){
    scoreText.text = "" + PlayerScore.GetScore(playerNum);
  }
}
