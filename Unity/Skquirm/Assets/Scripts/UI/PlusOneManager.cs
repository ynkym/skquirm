using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlusOneManager : MonoBehaviour {
  static PlusOneManager Instance;
  public static PlusOneManager GetInstance(){
    return Instance;
  }

  static float[] playerOffsetY = { 0, 0, -1, -1 };
  static float[] playerOffsetX = { -1, 0, -1, 0 };

  public int numCache = 10;
  public RectTransform parentCanvas;
  public GameObject prefab;
  public List<PlusOneBehavior> plusOnes;
  //public PlusOneBehavior[] plusOnes;

  private float halfwidth;
  private float halfheight;

  private int nextIndex;
  private int readyIndex;

  void Start(){
    Instance = this;
    plusOnes = new List<PlusOneBehavior>();
    for (int i=0 ; i<numCache; i++){
      GameObject go = (GameObject) Instantiate(prefab, prefab.transform.position, Quaternion.identity);
      go.transform.SetParent(parentCanvas, false);
      go.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
      go.transform.localPosition = Vector3.zero;
      plusOnes.Add(go.GetComponent<PlusOneBehavior>());
    }
    nextIndex = 0;
    readyIndex = -1;
  }

  public void DisplayPlusOne(int playerNum, Vector2 vpposition){
    if (readyIndex == nextIndex){
      Debug.Log("Ran out of PlusOne pool!!");
      return;
    }
    //Debug.Log(vpposition);
    halfwidth = parentCanvas.sizeDelta.x * 0.5f;
    halfheight = parentCanvas.sizeDelta.y * 0.5f;

    Vector2 srcPos = new Vector2(
      ((vpposition.x * halfwidth) + (playerOffsetX[playerNum] * halfwidth)),
      (vpposition.y * halfheight) + (playerOffsetY[playerNum] * halfheight));

    //now you can set the position of the ui element
    plusOnes[nextIndex].Display(srcPos);
    plusOnes[nextIndex].gameObject.SetActive(true);
    nextIndex = (nextIndex + 1) % numCache;
  }

  public void Ready(){
    readyIndex = (readyIndex + 1) % numCache;
  }
}
