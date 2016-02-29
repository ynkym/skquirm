using UnityEngine;
using System.Collections;

public class GlobalSetting : MonoBehaviour {

  // for singleton (am I doing this correctly?)
  private static GlobalSetting m_Instance;
  public static GlobalSetting Instance { get { return m_Instance; } }

    // this list keep track of all players which doesn't change in game
	private ArrayList players;
    public ArrayList getAllPlayers() { return players; }

    // this list only keep track of alive player
    private ArrayList alivePlayers;


  void Awake(){
    m_Instance = this;
        players = new ArrayList();
        alivePlayers = new ArrayList();
  }

  public void registerPlayer(GameObject player)
    {
        players.Add(player);
        alivePlayers.Add(player);
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

    // Keep track of the number of combined item
    private const int totalNumOfCombinedItem = 6;

    public static int getTotalNumOfCombinedItem()
    {
        return totalNumOfCombinedItem;
    }

    public GameObject ReturnTheNearest(GameObject currentPlayer){
		float min_distance;
		int min_index;
		float temp_dist;
		int i = 0;
        GameObject tempPlayer;

        if ((GameObject)alivePlayers[0] == currentPlayer) i++;
        tempPlayer = (GameObject)alivePlayers[i];
        min_distance = Vector3.Distance (currentPlayer.transform.position, tempPlayer.transform.position);
		min_index = i;

		for (int j = i+1; j < alivePlayers.Count; j++) {
			if ((GameObject) alivePlayers[j] == currentPlayer)
				continue;
            tempPlayer = (GameObject)alivePlayers[j];
            temp_dist = Vector3.Distance (currentPlayer.transform.position, tempPlayer.transform.position);
			if (min_distance > temp_dist) {
				min_distance = temp_dist;
				min_index = j;
			}
		}

		return (GameObject)alivePlayers[min_index];
	}

  void PlayerDefeated(GameObject defeatedPlayer){
        PlayerController pc = defeatedPlayer.GetComponent<PlayerController>();
    Debug.Log("Player " + pc.playerNum + " is defeated");
        alivePlayers.Remove(defeatedPlayer);
    if (alivePlayers.Count == 1){
      gameObject.SendMessage("ThereIsWinner");
    }
  }
}
