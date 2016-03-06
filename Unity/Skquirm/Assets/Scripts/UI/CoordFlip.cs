using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CoordFlip : MonoBehaviour {

  public int playerNum;
  static float[] playerScaleY = { 1, 1, -1, -1 };
  static float[] playerScaleX = { 1, -1, 1, -1 };

	// Use this for initialization
	void Start () {
    gameObject.transform.localScale = new Vector3(playerScaleX[playerNum], playerScaleY[playerNum], 1);
	}
}
