using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeStateUI : MonoBehaviour {

  static float[] angles = { Mathf.PI / 4, 3 * Mathf.PI / 8, Mathf.PI / 8 };
  static float[] playerScaleY = { 1, 1, -1, -1 };
  static float[] playerScaleX = { 1, -1, 1, -1 };

  static Dictionary<int, LifeStateUI> Instances = new Dictionary<int, LifeStateUI>();
  public static void UpdateForPlayer(int playerNum, int life){
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].UpdateUI(life);
    }
  }
  public static void Clear(){ Instances.Clear(); }

  public enum PlayerColor{ yellow, red, green, purple }
  public PlayerColor color;

  public int playerNum;
  public UnityEngine.UI.Image ringImage;

  private GameObject popObject;
  private ImageAnimation[] lives;
  private int prevLife;

    void Start () {
        // For when using ExecuteinEditMode... But doesn't work well when playing.
        // if (lives != null){
        //     for(int i = 0; i < 3 ; i++){
        //         Destroy(lives[i].gameObject);
        //         lives[i] = null;
        //     }
        // }

        // load assets
        string colorstring = color.ToString();
        popObject = (Resources.Load("Prefabs/BubblePop", (typeof(GameObject))) as GameObject);
        Texture2D bubbleSprite = (Resources.Load("Sprites/Life/circle_" + colorstring, (typeof(Texture2D))) as Texture2D);
        Sprite ringSprite = (Resources.Load("Sprites/Life/ring_" + colorstring, (typeof(Sprite))) as Sprite);

        // initialize variables
        lives = new ImageAnimation[3];
        prevLife = 3; // number of life

        // setup the whole UI
        gameObject.transform.localScale = new Vector3(playerScaleX[playerNum], playerScaleY[playerNum], 1);
        ringImage.sprite = ringSprite;

        // setup the individual lives
        float radius = 120;
        float offset = Mathf.PI / 2;
        for (int i = 0; i < 3 ; i++){
            GameObject lifeBubble = (GameObject) Instantiate(popObject, gameObject.transform.position, Quaternion.identity);
            lifeBubble.transform.SetParent(gameObject.transform);
            lifeBubble.transform.localPosition = new Vector3(radius * Mathf.Cos(angles[i] + offset),
                radius * Mathf.Sin(angles[i] + offset), 0);
            lifeBubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            lives[i] = lifeBubble.GetComponent<ImageAnimation>();
            lives[i].spriteSht = bubbleSprite;
        }

      if (Instances.ContainsKey(playerNum)){
        Instances[playerNum] = this;
      }else{
        Instances.Add(playerNum, this);
      }
    }

  public void UpdateUI (int life){
    for (int i = prevLife - 1; i >= life && i >= 0; i--){
        lives[i].StartAnim();
        lives[i] = null;
    }
    prevLife = life;
  }
}
