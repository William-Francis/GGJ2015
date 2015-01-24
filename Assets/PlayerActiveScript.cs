using UnityEngine;
using System.Collections;

public class PlayerActiveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );

			renderer.material.color = newColor;
		}
	}
}
