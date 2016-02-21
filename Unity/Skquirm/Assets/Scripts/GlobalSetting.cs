using UnityEngine;
using System.Collections;

public class GlobalSetting : MonoBehaviour {

  // for singleton (am I doing this correctly?)
  private static GlobalSetting m_Instance;
  public static GlobalSetting Instance { get { return m_Instance; } }

	public GameObject[] players;

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

  // number of times the offense-defense (OD) barrier can attack
	public int odAttackCount = 1;

  // the impulse force applied to the player at speed (S) item activation
  public float speedItemThrust = 15.0f;

	public GameObject ReturnTheNearest(GameObject currentPlayer){
		float min_distance;
		int min_index;
		float temp_dist;
		int i = 0;

		if (players [0] == currentPlayer) i++;

		min_distance = Vector3.Distance (currentPlayer.transform.position, players [i].transform.position);
		min_index = i;

		for (int j = i+1; j < players.Length; j++) {
			if (players [j] == currentPlayer)
				continue;
			temp_dist = Vector3.Distance (currentPlayer.transform.position, players [j].transform.position);
			if (min_distance > temp_dist) {
				min_distance = temp_dist;
				min_index = j;
			}
		}

		return players [min_index];
	}
}
