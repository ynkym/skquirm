using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ImageAnimation : MonoBehaviour {
  public Texture2D spriteSht;
  public int NUM_FRAMES_Y;
  public int NUM_FRAMES_X;
  public int TILE_UNIT_WIDTH;
  public int TILE_UNIT_HEIGHT;
  public float duration;

  private UnityEngine.UI.Image image;
  private List<Sprite> animFrames;

  private int numFrame;
  private float timeElapsed;
  private bool animStart;

	// Use this for initialization
	void Start () {
    animFrames = new List<Sprite>();
    SetupSpriteFrames();
    numFrame = animFrames.Count;

    image = gameObject.GetComponent<UnityEngine.UI.Image>();
    image.sprite = animFrames[0];

    timeElapsed = 0.0f;
    animStart = false;
	}

	// Update is called once per frame
	void Update () {
    if (animStart){
      timeElapsed = timeElapsed + Time.deltaTime;
      if (timeElapsed <= duration){
        int frame = Mathf.FloorToInt((timeElapsed / duration) * numFrame);
        image.sprite = animFrames[frame];
      }else{
        Destroy(this.gameObject);
      }
    }
	}

  public void StartAnim() {
    animStart = true;
  }

  // cut up the sprite sheet and store as sprites
  private void SetupSpriteFrames(){
    Vector2 pivot = new Vector2(0f, 0f);

    for (int j = 0; j < NUM_FRAMES_Y; j++){
      for (int i = 0; i < NUM_FRAMES_X; i++){
        Rect loc = new Rect(i * TILE_UNIT_WIDTH, (NUM_FRAMES_Y - (j+1)) * TILE_UNIT_HEIGHT, TILE_UNIT_WIDTH, TILE_UNIT_HEIGHT);
        Sprite s = Sprite.Create(spriteSht, loc, pivot, 1f);
        animFrames.Add(s);
      }
    }
  }

}
