using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LifeStateUI : MonoBehaviour {
  private UnityEngine.UI.Image image;
  private Sprite[] spriteTable;

	// Use this for initialization
	void Start () {
    spriteTable = new Sprite[4];
    spriteTable[3] = (AssetDatabase.LoadAssetAtPath("Assets/Sprites/life_3.jpg", (typeof(Sprite))) as Sprite);
    spriteTable[2] = (AssetDatabase.LoadAssetAtPath("Assets/Sprites/life_2.jpg", (typeof(Sprite))) as Sprite);
    spriteTable[1] = (AssetDatabase.LoadAssetAtPath("Assets/Sprites/life_1.jpg", (typeof(Sprite))) as Sprite);

    // spriteTable = new Dictionary<string, Sprite>();

    // spriteTable.Add("OffenseItem", ;
    // spriteTable.Add("DefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_D.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("SpeedItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_S.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("OffenseOffenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_OO.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("DefenseDefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_DD.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("SpeedSpeedItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_SS.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("OffenseSpeedItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_OS.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("OffenseDefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_OD.png", (typeof(Sprite))) as Sprite));
    // spriteTable.Add("SpeedDefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_SD.png", (typeof(Sprite))) as Sprite));
    // emptySprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_Empty.png", (typeof(Sprite))) as Sprite;
    // spriteTable.Add("default", emptySprite);

    image = gameObject.GetComponent<UnityEngine.UI.Image>();
	}

  public void UpdateUI (int life){
    image.sprite = spriteTable[life];
    // string itemtype;

    // if (item != null){
    //   itemtype = item.GetType().ToString();
    // } else {
    //   itemtype = "default";
    // }

    // if (spriteTable.ContainsKey(itemtype)){
    //   image.sprite = spriteTable[itemtype];
    // }else{
    //   image.sprite = emptySprite;
    // }
  }

	// Update is called once per frame
	void Update () {

	}
}
