 
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float glideSpeed = 100;
    public float floatSpeed = 40;
     private float scale;
	public GameObject bullet;
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
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
        {
			fireProjectile();
			GameObject bulletInstance = (GameObject) Instantiate(bullet, 
			                                 rigidbody2D.position , // need to add a space outside of current pig radius towards mouse click 
			                                 rigidbody2D.transform.rotation);
			bulletInstance.rigidbody2D.AddForce((new Vector2(mousePosition.x, mousePosition.y)-rigidbody2D.position)*100);
			//bulletInstance.transform.AddForce(rigidbody2D.transform.forward * force);
		}
		
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
	
	void fireProjectile()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Debug.DrawRay(rigidbody2D.position, new Vector2(mousePosition.x, mousePosition.y)-rigidbody2D.position ,Color.red);
	}
}
 