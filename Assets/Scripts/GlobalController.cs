using UnityEngine;
using System.Collections.Generic;

public enum PlayerState
{
    Pending,
    Joined,
    Eliminated
}

public class GlobalController : MonoBehaviour {

    private static GlobalController _instance;
    public static GlobalController Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject playerPrefab;

    public const int MAX_PLAYER_COUNT = 5;
    public PlayerState[] playerStates;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        playerStates = new PlayerState[MAX_PLAYER_COUNT];
        for (int i=0; i<MAX_PLAYER_COUNT; ++i)
        {
            playerStates[i] = PlayerState.Pending;
        }
    }

    public int playerCount
    {
        get
        {
            int result = 0;
            return result;
        }
    }

    public void setPlayerState(int player, PlayerState state)
    {
        playerStates[player] = state;
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p"))
		{
			Application.LoadLevel("testBorderScene");
		}
	}

    void spawnPlayer(int playerIndex, Vector3 position)
    {
        GameObject playerObj = (GameObject)Instantiate(playerPrefab);
        PlayerController control = playerObj.GetComponent<PlayerController>();
        control.playerID = playerIndex;
        control.transform.position = position;
    }

	void OnLevelWasLoaded(int level) {
        // Level 0 is the menu level
        // Level 1 is the inter-level score screen
        // Levels 2 - n+2 are the n levels
        if (level > 0)
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawnLoc");
            List<GameObject> spawnLocList = new List<GameObject>(spawnPoints);
            for (int i=0; i<MAX_PLAYER_COUNT; ++i)
            {
                if (playerStates[i] == PlayerState.Joined)
                {
                    int spawnIndex = Random.Range(0, spawnLocList.Count);
                    spawnPlayer(i, spawnLocList[spawnIndex].transform.position);
                    spawnLocList.RemoveAt(spawnIndex);
                }
            }
        }
	}

}
