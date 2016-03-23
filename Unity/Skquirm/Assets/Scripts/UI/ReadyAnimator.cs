using UnityEngine;
using System.Collections;

public class ReadyAnimator : MonoBehaviour {
  public int playerNum;

  void FistPumpEvent(){
    //Debug.Log("Ready!");
    ReadyButton.UpdateReadyForPlayer(playerNum, true);
  }
}
