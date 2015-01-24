using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
    public float glideSpeed = 100;
    public float floatSpeed = 40;

    private float scale;
    private float neutralScale = 0.5f; // Store the neutral scale to use as a zero-point for float acceleration

    void Awake()
    {
        scale = rigidbody2D.transform.localScale.x;
        //neutralScale = scale;
    }

    void kill()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            kill();
        }
    }

	void FixedUpdate()
	{
        // 3 - Retrieve axis information
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 5 - Move the game object
        // We want scale of 1 to result in 0 upwards acceleration
        // Clamp scale to a positive value so we don't sink directly due to scale
        float floatAcceleration = floatSpeed*scale;
        Vector2 newVelocity = rigidbody2D.velocity;
        newVelocity.x = glideSpeed*moveX;
        newVelocity.y = floatAcceleration*(scale-neutralScale) - 4f;
        rigidbody2D.velocity = newVelocity;

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
