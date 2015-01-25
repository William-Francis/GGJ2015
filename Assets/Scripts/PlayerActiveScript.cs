using UnityEngine;
using System.Collections.Generic;
using XboxCtrlrInput;
 
public class PlayerActiveScript : MonoBehaviour {

    public GameObject playerZero;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    public Sprite[] images;

    public GameObject playerZeroImage;
	public GameObject playerOneImage;
	public GameObject playerTwoImage;
	public GameObject playerThreeImage;
	public GameObject playerFourImage;

    private List<Sprite> imageList;

	// Use this for initialization
	void Start () {
        imageList = new List<Sprite>(images);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W) && GlobalController.Instance.playerStates[0] == PlayerState.Pending)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            playerZero.renderer.material.color = newColor;

            GlobalController.Instance.setPlayerState(0, PlayerState.Joined);
            int playerZeroImageIndex = Random.Range(0, imageList.Count);
            Debug.Log(playerZeroImageIndex);
            playerZeroImage.GetComponent<SpriteRenderer>().sprite = imageList[playerZeroImageIndex];
            imageList.RemoveAt(playerZeroImageIndex);
            playerZeroImage.SetActive(true);
        }
        if(((XCI.GetAxis(XboxAxis.LeftTrigger, 1) > 0.5f) || XCI.GetButtonDown(XboxButton.LeftBumper, 1))
             && GlobalController.Instance.playerStates[1] == PlayerState.Pending)
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerOne.renderer.material.color = newColor;

            GlobalController.Instance.setPlayerState(1, PlayerState.Joined);
            int playerOneImageIndex = Random.Range(0, imageList.Count);
            playerOneImage.GetComponent<SpriteRenderer>().sprite = imageList[playerOneImageIndex];
            imageList.RemoveAt(playerOneImageIndex);
            playerOneImage.SetActive(true);
        }
        if (((XCI.GetAxis(XboxAxis.RightTrigger, 1) > 0.5f) || XCI.GetButtonDown(XboxButton.RightBumper, 1))
             && GlobalController.Instance.playerStates[2] == PlayerState.Pending)
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerTwo.renderer.material.color = newColor;

            GlobalController.Instance.setPlayerState(2, PlayerState.Joined);
            int playerTwoImageIndex = Random.Range(0, imageList.Count);
            playerTwoImage.GetComponent<SpriteRenderer>().sprite = imageList[playerTwoImageIndex];
            imageList.RemoveAt(playerTwoImageIndex);
            playerTwoImage.SetActive(true);
        }
        if (((XCI.GetAxis(XboxAxis.LeftTrigger, 2) > 0.5f) || XCI.GetButtonDown(XboxButton.LeftBumper, 2))
             && GlobalController.Instance.playerStates[3] == PlayerState.Pending)
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerThree.renderer.material.color = newColor;

            GlobalController.Instance.setPlayerState(3, PlayerState.Joined);
            int playerThreeImageIndex = Random.Range(0, imageList.Count);
            playerThreeImage.GetComponent<SpriteRenderer>().sprite = imageList[playerThreeImageIndex];
            imageList.RemoveAt(playerThreeImageIndex);
            playerThreeImage.SetActive(true);
        }
        if (((XCI.GetAxis(XboxAxis.RightTrigger, 2) > 0.5f) || XCI.GetButtonDown(XboxButton.RightBumper, 2))
             && GlobalController.Instance.playerStates[4] == PlayerState.Pending)
        {
            Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
            playerFour.renderer.material.color = newColor;

            GlobalController.Instance.setPlayerState(4, PlayerState.Joined);
            int playerFourImageIndex = Random.Range(0, imageList.Count);
            playerFourImage.GetComponent<SpriteRenderer>().sprite = imageList[playerFourImageIndex];
            imageList.RemoveAt(playerFourImageIndex);
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
            GlobalController.Instance.totalPlayerCount = playerCount;
            if (playerCount >= 2)
            {
                Application.LoadLevel("level1");
            }
        }
	}
}
