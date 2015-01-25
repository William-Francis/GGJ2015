using UnityEngine;
using System.Collections.Generic;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float glideSpeed = 100;
    public float floatSpeed = 40;
    public float weight = 0.1f;
    private float scale;
    public GameObject bullet;
    private float neutralScale = 0.5f; // Store the neutral scale to use as a zero-point for float acceleration

    private float fireRate = 1.0f;
    private float nextFire = 0.0f;

    private float lastAimX;
    private float lastAimY;

	public AudioClip pigSnort1;
	public AudioClip pigSnort2;
	public AudioClip pigSnort3;
	public AudioClip pigSnort4;

	public AudioSource source;

    public Transform aimIndicator;

    public float BOUNCE_THRESHOLD = 0.2f;
    private List<Vector3> bounceList;
    private float lastBounceTime;

    private bool isDead = false;
    private float deadTime;
    private Vector3 deadMoveDir;

    void Awake()
    {
		source = GetComponent<AudioSource>();

        scale = rigidbody2D.transform.localScale.x;
        //neutralScale = scale;

        lastAimX = 0.0f;
        lastAimY = 1.0f;
        bounceList = new List<Vector3>();
        lastBounceTime = Time.time;
     }

    void kill()
    {
        GlobalController.Instance.playerStates[playerID] = PlayerState.Eliminated;
        GlobalController.Instance.killPlayer(playerID);

        isDead = true;
        deadTime = 0.0f;
        deadMoveDir = new Vector3(-rigidbody2D.velocity.x, -rigidbody2D.velocity.y, 0);
        Destroy(gameObject.transform.GetChild(0).gameObject); //Destroy the pointer
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
    }

    void deadUpdate()
    {
        deadTime += Time.fixedDeltaTime;
        transform.position += 50*deadMoveDir*Time.fixedDeltaTime;

        deadMoveDir.x += Random.Range(-1.0f, 1.0f);
        deadMoveDir.y += Random.Range(-1.0f, 1.0f);
        deadMoveDir.Normalize();

        Vector2 screenLoc = Camera.main.WorldToScreenPoint(transform.position);
        if (screenLoc.x < -50 || screenLoc.y < -50 || screenLoc.x > Camera.main.pixelWidth+50 || screenLoc.y > Camera.main.pixelHeight+50)
        {
            Destroy(gameObject);
        }
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

        PlayerController otherPlayer = coll.gameObject.GetComponent<PlayerController>();
        if(otherPlayer) // collision with player
        {
            float deltaBounce = Time.time - lastBounceTime;
            float otherDeltaBounce = Time.time - otherPlayer.lastBounceTime;
            if (deltaBounce > BOUNCE_THRESHOLD && otherDeltaBounce > BOUNCE_THRESHOLD)
            {
                source.PlayOneShot(pigSnort1,0.8f);

                lastBounceTime = Time.time;
                otherPlayer.lastBounceTime = Time.time;

                float mass1 = transform.localScale.x;
                float mass2 = coll.gameObject.transform.localScale.x;

                float velo1Scale = (mass1-mass2)/(mass1+mass2);
                float velo2Scale = (2*mass1)/(mass1+mass2);
                Vector2 newVelo1 = velo1Scale * coll.gameObject.rigidbody2D.velocity;
                Vector2 newVelo2 = velo2Scale * rigidbody2D.velocity;
                bounceList.Add(new Vector3(newVelo1.x, newVelo1.y, Time.time +1.0f));
                otherPlayer.bounceList.Add(new Vector3(newVelo2.x, newVelo2.y, Time.time +1.0f));
            }
        }
    }
    
    void FixedUpdate()
    {
        if (isDead)
        {
            deadUpdate();
            return;
        }

        // 3 - Retrieve axis information
        float moveX = 0;
        float moveY = 0;
        float aimX = 0;
        float aimY = 0;
        bool shoot = false;

        switch (playerID)
        {
            case(0):
                if (Input.GetKey(KeyCode.A)) moveX -= 1.0f;
                if (Input.GetKey(KeyCode.D)) moveX += 1.0f;
                if (Input.GetKey(KeyCode.W)) moveY += 1.0f;
                if (Input.GetKey(KeyCode.S)) moveY -= 1.0f;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePosition-transform.position;
                direction.z=0;
                direction.Normalize();
                aimX = direction.x;
                aimY = direction.y;
                shoot = Input.GetMouseButtonDown(0);
                break;

            case(1): case(3):
                if (XCI.GetDPad(XboxDPad.Left, (playerID+1)/2)) moveX -= 1.0f;
                if (XCI.GetDPad(XboxDPad.Right, (playerID+1)/2)) moveX += 1.0f;
                if (XCI.GetDPad(XboxDPad.Up, (playerID+1)/2)) moveY += 1.0f;
                if (XCI.GetDPad(XboxDPad.Down, (playerID+1)/2)) moveY -= 1.0f;
                aimX = XCI.GetAxis(XboxAxis.LeftStickX, (playerID+1)/2);
                aimY = XCI.GetAxis(XboxAxis.LeftStickY, (playerID+1)/2);
                shoot = ((XCI.GetAxis(XboxAxis.LeftTrigger, (playerID+1)/2) > 0.5f) ||
                            (XCI.GetButtonDown(XboxButton.LeftBumper, (playerID+1)/2)));
                break;

            case (2): case (4):
                if (XCI.GetButton(XboxButton.X, playerID/2)) moveX -= 1.0f;
                if (XCI.GetButton(XboxButton.B, playerID/2)) moveX += 1.0f;
                if (XCI.GetButton(XboxButton.Y, playerID/2)) moveY += 1.0f;
                if (XCI.GetButton(XboxButton.A, playerID/2)) moveY -= 1.0f;
                aimX = XCI.GetAxis(XboxAxis.RightStickX, playerID/2);
                aimY = XCI.GetAxis(XboxAxis.RightStickY, playerID/2);
                shoot = ((XCI.GetAxis(XboxAxis.RightTrigger, playerID/2) > 0.5f) ||
                            (XCI.GetButtonDown(XboxButton.RightBumper, playerID/2)));
                break;
        }

        if (aimX*aimX + aimY*aimY < 0.5f)
        {
            aimX = lastAimX;
            aimY = lastAimY;
        }
        else
        {
            lastAimX = aimX;
            lastAimY = aimY;
        }

        // Rotate aiming indicator
        float theta = Mathf.Atan2(aimY, aimX) * Mathf.Rad2Deg;
        aimIndicator.rotation = Quaternion.Euler(0, 0, theta-90);

        // Shoot
        if (shoot  && Time.time >= nextFire)
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

        ///Adding bounce factor from list
        for (int i = 0; i < bounceList.Count; i++)
        {
            if (bounceList[i].z <= Time.time)
            {
                bounceList.RemoveAt(i);
                i+=-1;
            }
            else
            {
                newVelocity.x += bounceList[i].x;
                newVelocity.y += bounceList[i].y;
            }
        }

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
