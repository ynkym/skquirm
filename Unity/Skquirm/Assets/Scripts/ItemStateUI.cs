using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStateUI : MonoBehaviour {
  public Sprite offense;
  public Sprite defense;
  public Sprite speed;
  public Sprite none;

  private UnityEngine.UI.Image image;
  private Dictionary<string, Sprite> spriteTable;

	// Use this for initialization
	void Start () {
    spriteTable = new Dictionary<string, Sprite>();

    spriteTable.Add("Offense", offense);
    spriteTable.Add("Defense", defense);
    spriteTable.Add("Speed", speed);

    image = gameObject.GetComponent<UnityEngine.UI.Image>();
    //image.sprite = speed;
	}

  void UpdateUI (string type){
    if (spriteTable.ContainsKey(type)){
      image.sprite = spriteTable[type];
    }else{
      image.sprite = none;
    }
  }

	// Update is called once per frame
	void Update () {

	}
}
