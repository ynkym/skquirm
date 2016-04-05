using UnityEngine;
using System.Collections;

public class CoinManagement : MonoBehaviour {
    static CoinManagement Instance;
    public static CoinManagement GetInstance(){
        return Instance;
    }
    public static GameObject InstantiateCoinAt(Vector3 position){
        return Instance.InstantiateCoinAtPosition(position);
    }

    public int numCoins;
    public GameObject coinPrefab;
    public CoinBehaviour[] coins;
    public Vector3[] directions;

    int current_index = 0;

	// Use this for initialization
	void Start () {
        Instance = this;
        coins = new CoinBehaviour[numCoins];
        for (int i = 0 ; i < numCoins ; i++){
            GameObject go = (GameObject) Instantiate(coinPrefab, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(this.gameObject.transform, false);
            coins[i] = go.GetComponent<CoinBehaviour>();
            coins[i].SetCoinManager(this.gameObject);
            go.SetActive(false);
        }
        current_index = 0;
	}

	// Update is called once per frame
	void Update () {
	}

    // Put all coins as children of this GO
    // right click in the editor menu and select Get All Coins
    [ContextMenu("Get All Coins")]
    public void GetAllCoins() {
        coins = GetComponentsInChildren<CoinBehaviour>();
    }

    // "Instantiate" the coins around the player's avatar
    [ContextMenu("Instantiate Coins")]
    public void InstantiateCoins(GameObject player_damaged, int spilled_coins = 5) {
        //if (current_index + spilled_coins < coins.Length)
        //{
            for (int i = 0; i < spilled_coins; i++)
            {
                coins[i].gameObject.SetActive(true);
                //coins[i].SetCoinManager(this.gameObject);
                coins[i].transform.parent = transform.parent;
                coins[i].ThrowCoin(player_damaged.transform.position + new Vector3(0f, 1.5f, 0f), directions[i%5]);

                // keep track of the index. loop by modulus.
                current_index  = (current_index + 1) % numCoins;

            }
            //current_index = current_index + 5;
        //}
        //else {
        //    current_index = 0;
        //}
    }

    public GameObject InstantiateCoinAtPosition(Vector3 position){
        coins[current_index].transform.position = position;
        coins[current_index].gameObject.SetActive(true);
        coins[current_index].mycollider.enabled = true;
        GameObject go = coins[current_index].gameObject;
        current_index  = (current_index + 1) % numCoins;
        return go;
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
