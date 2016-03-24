using UnityEngine;
using System.Collections;

public class CoinManagement : MonoBehaviour {

    public CoinBehaviour[] coins;

	// Use this for initialization
	void Start () {
	
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


}
