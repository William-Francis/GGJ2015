 
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float glideSpeed = 100;
    public float floatSpeed = 40;
	public float weight = 0.1f;
     private float scale;
	public GameObject bullet;
    private float neutralScale = 0.5f; // Store the neutral scale to use as a zero-point for float acceleration

	public float fireRate = 0.8f;
	private float nextFire = 0.0f;
	
	
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
        if (coll.gameObject.layer == 8) // hazard like spike
        {
            kill();
        }
		if (coll.gameObject.layer == 9) // bullet
		{
			weight+=3.0f;
			//add weight here
			//kill();
		}
	}
	
	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition-transform.position;
            direction.z = 0;
            direction.Normalize();
			fireProjectile(direction);
		}
		
        // 3 - Retrieve axis information
        float moveX = 0;
        float moveY = 0;

        switch (playerID)
        {
            case(0):
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
                break;

            case(1):
                moveX = Input.GetAxis("DPad_XAxis_1");
                moveY = Input.GetAxis("DPad_YAxis_1");
                break;

            case(2):
                moveX = Input.GetAxis("DPad_XAxis_2");
                moveY = Input.GetAxis("DPad_YAxis_2");
                break;

            case(3):
                moveX = Input.GetAxis("DPad_XAxis_3");
                moveY = Input.GetAxis("DPad_YAxis_3");
                break;

            case (4):
                moveX = Input.GetAxis("DPad_XAxis_4");
                moveY = Input.GetAxis("DPad_YAxis_4");
                break;
        }

        // 5 - Move the game object
        // We want scale of 1 to result in 0 upwards acceleration
        // Clamp scale to a positive value so we don't sink directly due to scale
        float floatAcceleration = floatSpeed*scale;
        Vector2 newVelocity = rigidbody2D.velocity;
        newVelocity.x = glideSpeed*moveX;
		newVelocity.y = floatAcceleration*(scale-neutralScale) - 4f -weight;
        rigidbody2D.velocity = newVelocity;

        scale += 0.5f*sign(moveY)*Time.fixedDeltaTime;
        scale = Mathf.Clamp(scale, 0.25f, 1.0f);

        rigidbody2D.transform.localScale = new Vector3(scale, scale, scale);
	}

    float sign(float f)
    {
        if (f == 0)
            return 0.0f;
        else if (f > 0)
            return 1.0f;
        else //if (f < 0)
            return -1.0f;
    }

	void fireProjectile(Vector3 direction)
	{
        Vector3 spawnLoc = transform.position + direction*scale*1.9f; //was 1.3f

        GameObject bulletInstance = (GameObject)Instantiate(bullet, spawnLoc, Quaternion.identity);
        // Should the bullet velocity be affected by the player's velocity?
        //bulletInstance.rigidbody2D.velocity = rigidbody2D.velocity;
        bulletInstance.rigidbody2D.AddForce(rigidbody2D.position+(new Vector2(direction.x, direction.y)*500));
	}
}
 