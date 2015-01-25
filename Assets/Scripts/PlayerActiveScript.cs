using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
 
public class PlayerActiveScript : MonoBehaviour {

    public GameObject playerZero;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    public GameObject playerZeroImage;
	public GameObject playerOneImage;
	public GameObject playerTwoImage;
	public GameObject playerThreeImage;
	public GameObject playerFourImage;

	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            playerZero.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(0, PlayerState.Joined);
            playerZeroImage.SetActive(true);
        }
        if((XCI.GetAxis(XboxAxis.LeftTrigger, 1) > 0.5f) || XCI.GetButtonDown(XboxButton.LeftBumper, 1))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerOne.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(1, PlayerState.Joined);
            playerOneImage.SetActive(true);
        }
        if ((XCI.GetAxis(XboxAxis.RightTrigger, 1) > 0.5f) || XCI.GetButtonDown(XboxButton.RightBumper, 1))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerTwo.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(2, PlayerState.Joined);
            playerTwoImage.SetActive(true);
        }
        if ((XCI.GetAxis(XboxAxis.LeftTrigger, 2) > 0.5f) || XCI.GetButtonDown(XboxButton.LeftBumper, 2))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerThree.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(3, PlayerState.Joined);
            playerThreeImage.SetActive(true);
        }
        if ((XCI.GetAxis(XboxAxis.RightTrigger, 2) > 0.5f) || XCI.GetButtonDown(XboxButton.RightBumper, 2))
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerFour.renderer.material.color = newColor;
            GlobalController.Instance.setPlayerState(4, PlayerState.Joined);
            playerFourImage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int playerCount = 0;
            for (int i=0; i<GlobalController.MAX_PLAYER_COUNT; ++i)
            {
                if (GlobalController.Instance.playerStates[i] == PlayerState.Joined)
                {
                    playerCount += 1;
                }
            }
            if (playerCount >= 2)
            {
                Application.LoadLevel("level1");
            }
        }
	}
}
