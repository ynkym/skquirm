using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinEffectBehavior : MonoBehaviour {

  static float timetoLive = 2f;

  private float elapsedTime;

  void Start(){
    elapsedTime = 0f;
    gameObject.SetActive(false);
  }

  void Update(){
    elapsedTime += Time.deltaTime;
    if (elapsedTime > timetoLive){
      // done, go back to pool
      EffectsManager.GetInstance().CoinEffectReady();
      elapsedTime = 0f;
      gameObject.SetActive(false);
    }
  }
}
