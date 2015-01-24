using UnityEngine;
using System.Collections;

public class ScoreScreenController : MonoBehaviour {

	public GameObject textOne;
	// Use this for initialization
	void Start () {	
		TextMesh t = (TextMesh)textOne.GetComponent(typeof(TextMesh));
		t.text =  GlobalController.Instance.scoreToString() ;// + " " + GlobalController.Instance.playerScore.ToString;
	}
	
	// Update is called once per frame
	void Update () {

		
		if (Input.GetKeyDown("space"))
		{			
			print(GlobalController.Instance.levelIndex);
			GlobalController.Instance.levelIndex +=1;
			print(GlobalController.Instance.levelIndex);
			GlobalController.Instance.resetPlayers();
			Application.LoadLevel("level"+GlobalController.Instance.levelIndex);

		}

		if (Input.GetKeyDown("r"))
		{
			GlobalController.Instance.totalReset();
			Application.LoadLevel("playerSelectScene");

		}
	}
}
