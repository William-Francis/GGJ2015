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
			Application.LoadLevel("level2");

		}

		if (Input.GetKeyDown("r"))
		{
			Application.LoadLevel("playerSelectScene");

		}
	}
}
