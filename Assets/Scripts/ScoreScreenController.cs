using UnityEngine;
using System.Collections;

public class ScoreScreenController : MonoBehaviour {

	public GameObject textOne;
	// Use this for initialization
	void Start () {
		TextMesh t = (TextMesh)textOne.GetComponent(typeof(TextMesh));
		t.text = "tester  " + GlobalController.deathList[0];// + " " + GlobalController.Instance.playerScore.ToString;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
