using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinnerIndicatorBehavior : MonoBehaviour {

  static Dictionary<int, WinnerIndicatorBehavior> Instances = new Dictionary<int, WinnerIndicatorBehavior>();
  public static void ShowForPlayer(int playerNum){
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].Show();
    }
  }
  public static void HideForPlayer(int playerNum){
    if (Instances.ContainsKey(playerNum)){
        Instances[playerNum].Hide();
    }
  }
  public static void Clear(){ Instances.Clear(); }

  public int playerNum;

  void Start () {
    Instances.Add(playerNum, this);
    gameObject.SetActive(false);
    PlayerScore.CheckWinnerForPlayer(playerNum);
  }

  void OnDestroy () {
    Instances.Remove(playerNum);
  }

  public void Show (){
    if (gameObject.activeSelf){ return; }
    gameObject.transform.localScale = Vector3.zero;
    gameObject.SetActive(true);
    iTween.ScaleTo(gameObject, iTween.Hash(
      "scale", Vector3.one,
      "islocal", true,
      "easetype", iTween.EaseType.easeInOutSine,
      "time", 1.0f));
  }

  public void Hide (){
    if (!gameObject.activeSelf){ return; }
    gameObject.transform.localScale = Vector3.one;
    iTween.ScaleTo(gameObject, iTween.Hash(
      "scale", Vector3.zero,
      "islocal", true,
      "easetype", iTween.EaseType.easeInOutSine,
      "time", 1.0f,
      "oncomplete", "DisableObject",
      "oncompletetarget", gameObject));
  }

  public void DisableObject(){
    gameObject.SetActive(false);
  }
}
