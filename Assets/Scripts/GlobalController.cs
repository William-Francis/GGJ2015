﻿using UnityEngine;
using System.Collections.Generic;

public enum PlayerState
{
    Pending,
    Joined,
    Eliminated
}


public class GlobalController : MonoBehaviour {

	public static List<int> deathList  = new List<int>();


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
	public int[] playerScore;


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
		playerScore= new int[MAX_PLAYER_COUNT];

        for (int i=0; i<MAX_PLAYER_COUNT; ++i)
        {
            playerStates[i] = PlayerState.Pending;
			playerScore[i] = 0;
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

	public string scoreToString()
	{
		string fullString ="\n";

		for (int i=0; i<MAX_PLAYER_COUNT; ++i)
		{
			if(playerStates[i]!=PlayerState.Pending)
			{
			fullString += "Player " + i +" score: " +playerScore[i] + "\n";
			}
		}

		return fullString;
	}

    public void setPlayerState(int player, PlayerState state)
    {
        playerStates[player] = state;
    }

	public void killPlayer(int playerIndex)
	{
		deathList.Add(playerIndex);
		playerStates[playerIndex] = PlayerState.Eliminated;
		int count =0;
		int lastAlive =0;
		for (int i=0; i<MAX_PLAYER_COUNT; ++i)
		{
		if(	playerStates[i] == PlayerState.Joined)
			{
				lastAlive = i;
				count++;
			}
 		}

		if(count<=1) // all players are dead
		{		 
				 
				playerScore[lastAlive] += 3;
				 
				playerScore[deathList[deathList.Count-1]]+=2; // second last to die
				
				if(playerCount>2)
				{
				playerScore[deathList[deathList.Count-2]]+=1; // third
				}
			Application.LoadLevel("scoreScreen");
		}

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
