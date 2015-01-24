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

	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("A_1"))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerOne.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(0, PlayerState.Joined);
            playerOneImage.SetActive(true);
        }
        if (Input.GetButtonDown("A_2"))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerTwo.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(1, PlayerState.Joined);
            playerTwoImage.SetActive(true);
        }
        if (Input.GetButtonDown("A_3"))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerThree.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(2, PlayerState.Joined);
            playerThreeImage.SetActive(true);
        }
        if (Input.GetButtonDown("A_4"))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerFour.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(3, PlayerState.Joined);
            playerFourImage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("level1");
        }
	}
}
