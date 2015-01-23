using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(2, 2);
	
	// 2 - Store the movement
	private Vector2 movement;
	
	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// 4 - Movement per direction
		movement = new Vector2(
			speed.x * inputX / 10,
			speed.y * inputY / 20);
		if(Input.GetKey(KeyCode.W))
		{
			rigidbody2D.transform.localScale+= new Vector3(0.5f,0.5f,0.5f);
		}
		if(Input.GetKey(KeyCode.S))
		{
			if(rigidbody2D.transform.localScale.x>0){
			rigidbody2D.transform.localScale+= new Vector3(-0.5f,-0.5f,-0.5f);
			}
		}
	}

	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement + new Vector2(0 , rigidbody2D.transform.localScale.x -5.5f );
		//rigidbody2D.velocity += new Vector3(0,1 ,0);
	}
}
