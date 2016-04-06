using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemStateUI : MonoBehaviour {
  static Dictionary<int, ItemStateUI> Instances = new Dictionary<int, ItemStateUI>();
  public static void UpdateForPlayer(int playerNum, Item item){
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].UpdateUI(item);
    }
  }
  public static void Clear(){ Instances.Clear(); }

  public static Color semiWhite = Color.white;

  public int playerNum;

  private Sprite emptySprite;
  private UnityEngine.UI.Image image;
  private Dictionary<string, Sprite> spriteTable;

  public UnityEngine.UI.Image maskImage;

	// Use this for initialization
	void Start () {
    spriteTable = new Dictionary<string, Sprite>();

    spriteTable.Add("OffenseItem", (Resources.Load("Sprites/Item/item_O", (typeof(Sprite))) as Sprite));
    spriteTable.Add("DefenseItem", (Resources.Load("Sprites/Item/item_D", (typeof(Sprite))) as Sprite));
    spriteTable.Add("SpeedItem", (Resources.Load("Sprites/Item/item_S", (typeof(Sprite))) as Sprite));
    spriteTable.Add("OffenseOffenseItem", (Resources.Load("Sprites/Item/item_OO", (typeof(Sprite))) as Sprite));
    spriteTable.Add("DefenseDefenseItem", (Resources.Load("Sprites/Item/item_DD", (typeof(Sprite))) as Sprite));
    spriteTable.Add("SpeedSpeedItem", (Resources.Load("Sprites/Item/item_SS", (typeof(Sprite))) as Sprite));
    spriteTable.Add("OffenseSpeedItem", (Resources.Load("Sprites/Item/item_OS", (typeof(Sprite))) as Sprite));
    spriteTable.Add("OffenseDefenseItem", (Resources.Load("Sprites/Item/item_OD", (typeof(Sprite))) as Sprite));
    spriteTable.Add("SpeedDefenseItem", (Resources.Load("Sprites/Item/item_SD", (typeof(Sprite))) as Sprite));
    emptySprite = Resources.Load("Sprites/Item/item_Empty", (typeof(Sprite))) as Sprite;
    spriteTable.Add("default", emptySprite);

    image = gameObject.GetComponent<UnityEngine.UI.Image>();
    maskImage = transform.parent.gameObject.GetComponent<UnityEngine.UI.Image>();
    image.sprite = emptySprite;

    if (Instances.ContainsKey(playerNum)){
      Instances[playerNum] = this;
    }else{
      Instances.Add(playerNum, this);
    }

    semiWhite.a = 0.5f;
    image.color = semiWhite;
    maskImage.color = semiWhite;
	}

  public void UpdateUI (Item item){
    string itemtype;

    if (item != null){
      itemtype = item.GetType().ToString();
      image.color = Color.white;
      maskImage.color = Color.white;
    } else {
      itemtype = "default";
      image.color = semiWhite;
      maskImage.color = semiWhite;
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
