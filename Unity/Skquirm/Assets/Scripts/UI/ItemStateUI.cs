using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ItemStateUI : MonoBehaviour {
  static Dictionary<int, ItemStateUI> Instances = new Dictionary<int, ItemStateUI>();
  public static void UpdateForPlayer(int playerNum, Item item){
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].UpdateUI(item);
    }
  }

  public int playerNum;

  private Sprite emptySprite;
  private UnityEngine.UI.Image image;
  private Dictionary<string, Sprite> spriteTable;

	// Use this for initialization
	void Start () {
    spriteTable = new Dictionary<string, Sprite>();

    spriteTable.Add("OffenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_O.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("DefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_D.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("SpeedItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_S.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("OffenseOffenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_OO.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("DefenseDefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_DD.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("SpeedSpeedItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_SS.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("OffenseSpeedItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_OS.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("OffenseDefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_OD.png", (typeof(Sprite))) as Sprite));
    spriteTable.Add("SpeedDefenseItem", (AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_SD.png", (typeof(Sprite))) as Sprite));
    emptySprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/item_Empty.png", (typeof(Sprite))) as Sprite;
    spriteTable.Add("default", emptySprite);

    image = gameObject.GetComponent<UnityEngine.UI.Image>();
    image.sprite = emptySprite;

    Instances.Add(playerNum, this);
	}

  public void UpdateUI (Item item){
    string itemtype;

    if (item != null){
      itemtype = item.GetType().ToString();
    } else {
      itemtype = "default";
    }

    if (spriteTable.ContainsKey(itemtype)){
      image.sprite = spriteTable[itemtype];
    }else{
      image.sprite = emptySprite;
    }
  }

	// Update is called once per frame
	void Update () {

	}
}
