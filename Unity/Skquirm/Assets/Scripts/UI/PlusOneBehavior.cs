using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlusOneBehavior : MonoBehaviour {

  static float timetoLive = 1f;
  static float vertShift = 40f;

  private float elapsedTime;
  private Color color = Color.white;
  public UnityEngine.UI.Image image;

  void Start(){
    gameObject.SetActive(false);
  }

  void Update(){
    elapsedTime += Time.deltaTime;
    if (elapsedTime > timetoLive){
      // done, go back to pool
      PlusOneManager.GetInstance().Ready();
      gameObject.SetActive(false);
    }else{
      color.a = 1 - Mathf.Pow((elapsedTime / timetoLive), 2);
      image.color = color;
      Vector2 temp = transform.localPosition;
      temp.y += vertShift * (Time.deltaTime / timetoLive);
      transform.localPosition = temp;
    }
  }

  public void Display(Vector2 srcPos){ //"activate" the object
    elapsedTime = 0f;
    transform.localPosition = srcPos;
    color.a = 1;
    image.color = color;
    gameObject.SetActive(true);
  }
}
