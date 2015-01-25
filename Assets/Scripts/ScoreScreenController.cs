using UnityEngine;
using System.Collections;

public class ScoreScreenController : MonoBehaviour {

	public GameObject textOne;

	public AudioClip pigSqueal1; 
	public AudioClip pigSqueal2; 
	public AudioClip pigSqueal3; 
	public AudioClip pigSqueal4; 
 
	public AudioSource source;


	// Use this for initialization
	void Start () {	
		TextMesh t = (TextMesh)textOne.GetComponent(typeof(TextMesh));
		t.text =  GlobalController.Instance.scoreToString() ;// + " " + GlobalController.Instance.playerScore.ToString;

		switch(Random.Range(0, 4))
		{
		case 1:
			source.PlayOneShot(pigSqueal1,0.5f);
			break;
		case 2:
			source.PlayOneShot(pigSqueal2,0.5f);
			break;
		case 3:
			source.PlayOneShot(pigSqueal3,0.5f);
			break;
		case 4:
			source.PlayOneShot(pigSqueal4,0.5f);
			break;
		default:
			source.PlayOneShot(pigSqueal1,0.5f);
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {

		
		if (Input.GetKeyDown("space"))
		{			
			Application.LoadLevel("level"+GlobalController.Instance.getNextLevel());
		}

		if (Input.GetKeyDown("r"))
		{
			GlobalController.Instance.totalReset();
			Application.LoadLevel("playerSelectScene");

		}
	}
}
