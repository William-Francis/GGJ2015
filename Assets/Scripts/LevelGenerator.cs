using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public Transform wallObject;
    public Transform spikeObject;
    public Transform brandObject;
    public Transform fanObject;

    private const float LEVEL_WIDTH = 50.0f;
    private const float LEVEL_HEIGHT = 25.0f;

    public void genLevel1()
    {
        Transform parent = new GameObject("World").transform;

        //Left and Right
        for (int y=0; y<11; y++)
        {
            float yCoord = y*2.56f;
            Transform obj;

            obj = (Transform)Instantiate(wallObject, new Vector3(0, yCoord, 0), Quaternion.identity);
            obj.parent = parent;

            obj = (Transform)Instantiate(wallObject, new Vector3(50, yCoord, 0), Quaternion.identity);
            obj.parent = parent;
        }


        // Top and Bottom
        for (int x=0; x<100; ++x)
        {
            float xCoord = x*0.5f;
            Transform obj;

            obj = (Transform)Instantiate(spikeObject, new Vector3(xCoord, 25, 0), Quaternion.identity);
            obj.parent = parent;
            obj.localRotation = Quaternion.Euler(0, 0, 180);

            obj = (Transform)Instantiate(spikeObject, new Vector3(xCoord, 0, 0), Quaternion.identity);
            obj.parent = parent;

            
        }

        // Top and bottom Walls
        for (int x=0; x<21; ++x)
        {
            float xCoord = x*2.56f;
            Transform obj;

            obj = (Transform)Instantiate(wallObject, new Vector3(xCoord, -2.56f, 0), Quaternion.identity);
            obj.parent = parent;

            obj = (Transform)Instantiate(wallObject, new Vector3(xCoord, 27.56f, 0), Quaternion.identity);
            obj.parent = parent;
        }
    }

    public void genLevel2()
    {
        Transform parent = new GameObject("World").transform;

        //Left and Right
        for (int y=0; y<11; y++)
        {
            float yCoord = y*2.56f;
            Transform obj;

            obj = (Transform)Instantiate(wallObject, new Vector3(0, yCoord, 0), Quaternion.identity);
            obj.parent = parent;

            obj = (Transform)Instantiate(wallObject, new Vector3(50, yCoord, 0), Quaternion.identity);
            obj.parent = parent;
        }


        // Top and Bottom
        for (int x=0; x<100; ++x)
        {
            float xCoord = x*0.5f;
            Transform obj;

            obj = (Transform)Instantiate(spikeObject, new Vector3(xCoord, 25, 0), Quaternion.identity);
            obj.parent = parent;
            obj.localRotation = Quaternion.Euler(0, 0, 180);

            obj = (Transform)Instantiate(spikeObject, new Vector3(xCoord, 0, 0), Quaternion.identity);
            obj.parent = parent;


        }

        // Top and bottom Walls
        for (int x=0; x<21; ++x)
        {
            float xCoord = x*2.56f;
            Transform obj;

            obj = (Transform)Instantiate(wallObject, new Vector3(xCoord, -2.56f, 0), Quaternion.identity);
            obj.parent = parent;

            obj = (Transform)Instantiate(wallObject, new Vector3(xCoord, 27.56f, 0), Quaternion.identity);
            obj.parent = parent;
        }


        // Middle block
        for (int x=1; x<20; ++x)
        {
            if (x > 7 && x < 13)
                continue;

            float xCoord = x*2.56f;
            Transform obj;
            float yCoord = 5*2.56f;
            obj = (Transform)Instantiate(wallObject, new Vector3(xCoord, yCoord, 0), Quaternion.identity);
            obj.parent = parent;
        }

        for (int x=1; x<100; ++x)
        {
            if (x > 37 && x < 65)
                continue;

            float xCoord = x*0.5f;
            Transform obj;
            float yCoord = 5*2.56f;
            obj = (Transform)Instantiate(spikeObject, new Vector3(xCoord, yCoord+2.56f, 0), Quaternion.identity);
            obj.parent = parent;

            obj = (Transform)Instantiate(spikeObject, new Vector3(xCoord, yCoord-2.56f, 0), Quaternion.identity);
            obj.parent = parent;
            obj.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public void genLevel3()
    {

    }

    public void genLevel4()
    {

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            genLevel1();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            genLevel2();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            genLevel3();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            genLevel4();
        }
	}
}
