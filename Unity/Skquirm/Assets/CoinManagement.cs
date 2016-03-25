using UnityEngine;
using System.Collections;

public class CoinManagement : MonoBehaviour {

    public CoinBehaviour[] coins;
    public GameObject[] avatar;
    public Vector3[] directions;

    int current_index = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            InstantiateCoins();
        }
	}

    // Put all coins as children of this GO
    // right click in the editor menu and select Get All Coins
    [ContextMenu("Get All Coins")]
    public void GetAllCoins() {
        coins = GetComponentsInChildren<CoinBehaviour>();
    }

    // "Instantiate" the coins around the player's avatar
    [ContextMenu("Instantiate Coins")]
    public void InstantiateCoins() {
        if (current_index + 5 < coins.Length)
        {
            for (int i = current_index; i < current_index + 5; i++)
            {
                coins[i].gameObject.SetActive(true);
                coins[i].transform.parent = transform.parent;
                coins[i].ThrowCoin(avatar[0].transform.position + new Vector3(0f, 1.5f, 0f), directions[i%5]);
            }
            current_index = current_index + 5;
        }
        else {
            current_index = 0;
        }
    }

    //Get the directions: temporary function to get the direction vectors easier
    //[ContextMenu("GetDirections")]
    /*
    public void GetDirections() {
        directions = new Vector3[targets.Length];

        for (int i = 0; i < targets.Length; i++) {
            directions[i] = avatar[0].transform.position - targets[i].transform.position;
        }
    }*/


}
