 
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

	public float fireRate = 0.001f;
	private float nextFire = 0.0f;
 
	void Awake()
    {
        scale = rigidbody2D.transform.localScale.x;
        //neutralScale = scale;
     }

    void kill()
    {
		GlobalController.Instance.playerStates[playerID] = PlayerState.Eliminated;
		GlobalController.Instance.killPlayer(playerID);

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
			weight+=1.0f;
			coll.transform.parent = transform;
			coll.rigidbody.isKinematic=true;
			coll.collider.enabled=false;
			rigidbody2D.rigidbody2D.mass+=1;
			//add weight here
			//kill();
		}
	}
	
	void FixedUpdate()
	{
        // 3 - Retrieve axis information
        float moveX = 0;
        float moveY = 0;
        float aimX = 0;
        float aimY = 0;
        bool shoot = false;

        switch (playerID)
        {
            case(0):
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePosition-transform.position;
			direction.z=0;
			direction.Normalize();
                aimX = direction.x;
                aimY = direction.y;
                shoot = Input.GetMouseButtonDown(0);
                break;

            case(1): case(2): case(3): case(4):
                moveX = Input.GetAxis("DPad_XAxis_"+playerID);
                moveY = Input.GetAxis("DPad_YAxis_"+playerID);
                aimX = Input.GetAxis("L_XAxis_"+playerID);
                aimY = Input.GetAxis("L_YAxis_"+playerID);
                shoot = ((Input.GetAxis("TriggersL_"+playerID) > 0.5f) || (Input.GetButtonDown("LB_"+playerID)));
                break;
        }

        // Shoot
        if (shoot  )
        {
            nextFire = Time.time + fireRate;
            fireProjectile(aimX, aimY);
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
        scale = Mathf.Clamp(scale, 0.25f, 1.25f);

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

	void fireProjectile(float x, float y)
	{
        Vector3 spawnLoc = transform.position;
        spawnLoc.x += x*scale*1.9f;
        spawnLoc.y += y*scale*1.9f;

        GameObject bulletInstance = (GameObject)Instantiate(bullet, spawnLoc, Quaternion.identity);
        // Should the bullet velocity be affected by the player's velocity?
		Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
		bulletInstance.renderer.material.color = newColor;
		bulletInstance.rigidbody2D.AddForce(rigidbody2D.position+(new Vector2(x, y)*500));
	}
}
 