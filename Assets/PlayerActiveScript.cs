using UnityEngine;
using System.Collections;

public class PlayerActiveScript : MonoBehaviour {

	public GameObject playerOne;
	public GameObject playerTwo;
	public GameObject playerThree;
	public GameObject playerFour;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			 playerOne.renderer.material.color = newColor;
		}
		if (Input.GetMouseButtonDown(1))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			playerTwo.renderer.material.color = newColor;
		}
		if (Input.GetMouseButtonDown(2))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			playerThree.renderer.material.color = newColor;
		}
		if (Input.GetMouseButtonDown(3))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			playerFour.renderer.material.color = newColor;
		}
	}
}
