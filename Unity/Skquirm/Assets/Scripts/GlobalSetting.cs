using UnityEngine;
using System.Collections;

public class GlobalSetting : MonoBehaviour {

  // for singleton (am I doing this correctly?)
  private static GlobalSetting m_Instance;
  public static GlobalSetting Instance { get { return m_Instance; } }

  void Awake(){
    m_Instance = this;
  }

  void OnDestroy(){
    m_Instance = null;
  }

  // time the defense (D) barrier will hold in seconds
  public float defenseBarrierDuration = 5.0f;

  // time the defense (DD) barrier will hold in seconds
  public float ddBarrierDuration = 5.0f;

  // number of times the defense (D) barrier will withstand attacks
  public int defenseBarrierCount = 1;

  // the impulse force applied to the player at speed (S) item activation
  public float speedItemThrust = 15.0f;
}