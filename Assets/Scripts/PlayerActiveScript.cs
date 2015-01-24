using UnityEngine;
using System.Collections;
 
public class PlayerActiveScript : MonoBehaviour {

	public GameObject playerOne;
	public GameObject playerTwo;
	public GameObject playerThree;
	public GameObject playerFour;

	public GameObject playerOneImage;
	public GameObject playerTwoImage;
	public GameObject playerThreeImage;
	public GameObject playerFourImage;

	public int playerOneInt =0;
	public int playerTwoInt=0;
	public int playerThreeInt=0;
	public int playerFourInt=0;

	//public GlobalController gameController;

	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		//gameController.setNumberOfPlayers(2 );



		if (Input.GetMouseButtonDown(0))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			 playerOne.renderer.material.color = newColor;
			playerOneInt=1;
			playerOneImage.SetActive(true);
		}
		if (Input.GetMouseButtonDown(1))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			playerTwo.renderer.material.color = newColor;
			playerTwoInt=1;
			playerTwoImage.SetActive(true);

		}
		if (Input.GetMouseButtonDown(2))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			playerThree.renderer.material.color = newColor;
			playerThreeInt=1;
			playerThreeImage.SetActive(true);

		}
		if (Input.GetMouseButtonDown(3))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			playerFour.renderer.material.color = newColor;
			playerFourInt=1;
			playerFourImage.SetActive(true);

		}
		if (Input.GetKeyDown("space"))
		{
			GlobalController.setNumberOfPlayers(playerOneInt+playerTwoInt+playerThreeInt+playerFourInt );
			Application.LoadLevel("testBorderScene");
		}

	}
}
