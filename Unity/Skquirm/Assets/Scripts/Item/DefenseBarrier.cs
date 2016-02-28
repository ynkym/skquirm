using UnityEngine;
using System.Collections;

public class DefenseBarrier : MonoBehaviour {

  protected GameObject barrier;
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
        DestroyBarrier();
      }
    }
  }

  void DestroyBarrier () {
        iTween.ScaleTo(barrier, Vector3.zero, 0.5f);
        StartCoroutine(DestroyBarrierAfter(0.6f));
  }

    // Create a delay to destroy barrier
    IEnumerator DestroyBarrierAfter(float time) {
        yield return new WaitForSeconds(time);
        Destroy(barrier);
        Destroy(this);
    }

  // Set to negative values for infinite count or infinite time
  public void setupDefenseBarrier (float time, int count) {
    remainingTime = time;
    barrierCount = count;
  }

  public void breakingBarrier () {
    barrierCount = barrierCount - 1;
    if (barrierCount == 0){
      DestroyBarrier();
    }
  }
}
