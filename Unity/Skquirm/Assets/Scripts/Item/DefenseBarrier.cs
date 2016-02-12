using UnityEngine;
using System.Collections;

public class DefenseBarrier : MonoBehaviour {

  private GameObject barrier;
  public float remainingTime;
  public int barrierCount;

  // Use this for initialization
  void Start () {
    GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Barrier.prefab", (typeof(GameObject))) as GameObject;
    barrier = (GameObject) Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    barrier.transform.parent = gameObject.transform;
  }

  // Update is called once per frame
  void Update () {
    if (remainingTime > 0){
      remainingTime = remainingTime - Time.deltaTime;
      if (remainingTime <= 0){
        this.DestroyBarrier();
      }
    }
  }

  void DestroyBarrier () {
    Destroy(barrier);
    Destroy(this);
  }

  // Set to negative values for infinite count or infinite time
  public void setupDefenseBarrier (float time, int count) {
    remainingTime = time;
    barrierCount = count;
  }

  public void hitBarrier () {
    barrierCount = barrierCount - 1;
    if (barrierCount == 0){
      this.DestroyBarrier();
    }
  }
}
