using UnityEngine;
using System.Collections;

public class DefenseBarrier : MonoBehaviour {

  protected GameObject barrier;
  public float remainingTime;
  public int barrierCount;

  // Use this for initialization
  void Start () {
    GameObject prefab = Resources.Load("Prefabs/Barrier/Barrier", (typeof(GameObject))) as GameObject;
    barrier = (GameObject) Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    barrier.transform.parent = gameObject.transform;
  }

  // Update is called once per frame
  void Update () {
    if (remainingTime > 0){
      remainingTime = remainingTime - Time.deltaTime;
      if (remainingTime <= 0){
        DestroyBarrier();
      }
    }
  }

  void DestroyBarrier () {
        Destroy(barrier);
        GameObject prefab = Resources.Load("Prefabs/Barrier/BarrierClosing", (typeof(GameObject))) as GameObject;
        GameObject tempBarrier = Instantiate(prefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        tempBarrier.transform.parent = gameObject.transform;
        Destroy(this);
  }

  // Set to negative values for infinite count or infinite time
  public void setupDefenseBarrier (float time, int count) {
    remainingTime = time;
    barrierCount = count;
  }

  public void breakingBarrier () {
    barrierCount--;
    if (barrierCount == 0){
      DestroyBarrier();
    }
  }
}
