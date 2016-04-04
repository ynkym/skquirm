using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemEffectBehavior : MonoBehaviour {

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
      EffectsManager.GetInstance().ItemEffectReady();
      elapsedTime = 0f;
      gameObject.SetActive(false);
    }
  }
}
