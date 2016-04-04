using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectsManager : MonoBehaviour {
  static EffectsManager Instance;
  public static EffectsManager GetInstance(){
    return Instance;
  }

  // item effect
  public GameObject itemEffectPrefab;
  public int itemEffectPoolSize;
  public List<GameObject> itemEffectPool;
  private int itemEffectNextIndex;
  private int itemEffectReadyIndex;

  // coin effect
  public GameObject coinEffectPrefab;
  public int coinEffectPoolSize;
  public List<GameObject> coinEffectPool;
  private int coinEffectNextIndex;
  private int coinEffectReadyIndex;

  void Start(){
    Instance = this;

    //initialize item effect pool
    itemEffectPool = new List<GameObject>();
    for (int i=0 ; i<itemEffectPoolSize; i++){
      GameObject go = (GameObject) Instantiate(itemEffectPrefab, itemEffectPrefab.transform.position, Quaternion.identity);
      go.transform.parent = gameObject.transform;
      go.transform.localScale = new Vector3(1f, 1f, 1f);
      itemEffectPool.Add(go);
    }
    itemEffectNextIndex = 0;
    itemEffectReadyIndex = -1;

    //initialize coin effect pool
    coinEffectPool = new List<GameObject>();
    for (int i=0 ; i<coinEffectPoolSize; i++){
      GameObject go = (GameObject) Instantiate(coinEffectPrefab, coinEffectPrefab.transform.position, Quaternion.identity);
      go.transform.parent = gameObject.transform;
      go.transform.localScale = new Vector3(1f, 1f, 1f);
      coinEffectPool.Add(go);
    }
    coinEffectNextIndex = 0;
    coinEffectReadyIndex = -1;
  }

  public void DisplayItemEffect(Vector3 position){
    if (itemEffectReadyIndex == itemEffectNextIndex){
      Debug.Log("Ran out of Item Effect pool!!");
      return;
    }
    itemEffectPool[itemEffectNextIndex].transform.position = position;
    itemEffectPool[itemEffectNextIndex].SetActive(true);
    itemEffectNextIndex = (itemEffectNextIndex + 1) % itemEffectPoolSize;
  }

  public void ItemEffectReady(){
    itemEffectReadyIndex = (itemEffectReadyIndex + 1) % itemEffectPoolSize;
  }

  public void DisplayCoinEffect(Vector3 position){
    if (coinEffectReadyIndex == coinEffectNextIndex){
      Debug.Log("Ran out of coin Effect pool!!");
      return;
    }
    coinEffectPool[coinEffectNextIndex].transform.position = position;
    coinEffectPool[coinEffectNextIndex].SetActive(true);
    coinEffectNextIndex = (coinEffectNextIndex + 1) % coinEffectPoolSize;
  }

  public void CoinEffectReady(){
    coinEffectReadyIndex = (coinEffectReadyIndex + 1) % coinEffectPoolSize;
  }
}
