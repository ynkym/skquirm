using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

  public UnityEngine.UI.Image[] letters;
  public float timePerLetter = 0.5f;
  private int nextind;
  private float count;
  private bool start;

	// Use this for initialization
	void Start () {
    count = 0f;
    start = true;
    nextind = 0;
	}

	// Update is called once per frame
	void Update () {
    if (start){
      count = count + Time.deltaTime;
      while (timePerLetter * nextind < count){
        if (nextind >= letters.Length){
          start = false;
          GoToScene.startGame("Tank");
          break;
        }else{
          letters[nextind].gameObject.SetActive(true);
          nextind += 1;
        }

      }
    }
	}
}
