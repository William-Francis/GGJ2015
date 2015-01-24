using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {


	public static int numberOfPlayers =1;
	
	public static void setNumberOfPlayers( int number)
	{
		numberOfPlayers = number;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p"))
		{
			Application.LoadLevel("testBorderScene");
		}
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1)
			print("Woohoo");
		
	}

}
