using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(2, 2);

    public float floatSpeed = 2;

    private float scale;
    private float neutralScale; // Store the neutral scale to use as a zero-point for float acceleration

    void Awake()
    {
        scale = rigidbody2D.transform.localScale.x;
        neutralScale = scale;
    }

	void FixedUpdate()
	{
        // 3 - Retrieve axis information
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 4 - Movement per direction
        Vector2 movement = new Vector2(speed.x * moveX / 10, speed.y * moveY / 20);

        // 5 - Move the game object
        // We want scale of 1 to result in 0 upwards acceleration
        // Clamp scale to a positive value so we don't sink directly due to scale
        float floatAcceleration = floatSpeed*Mathf.Max(0,(scale - neutralScale));
        rigidbody2D.velocity = movement + new Vector2(0, floatAcceleration);

        if (Input.GetKey(KeyCode.W))
        {
            scale += 0.5f*Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            scale -= 0.5f*Time.fixedDeltaTime;
        }
        scale = Mathf.Clamp(scale, 0.25f, 1.0f);

        rigidbody2D.transform.localScale = new Vector3(scale, scale, scale);
	}
}
