using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpread : MonoBehaviour
{
    public GameObject[] spriteMasks;
    private RoundHandler roundHandler;

    private int minRange = 0;
    private int maxRange = 1;
    private int wave;
    private bool inWave;

    public int maxX;
    public int minX;
    public int maxY;
    public int minY;

    public float speed = 1;

    private List<GameObject> maskList = new List<GameObject>();
    private List<Vector3> pointsList;

    private float parentXScale;
    private float parentYScale;

    private void Start()
    {
        roundHandler = GameObject.FindObjectOfType<RoundHandler>();

        //find all points in range (in border and between min and max)
        pointsList = findAllPoints(this.transform.position);
        //parentXScale = GetComponentInParent<Transform>().localScale.x;
        //parentYScale = GetComponentInParent<Transform>().localScale.y;

        //initialSpawn();
    }

    private void Update()
    {
        wave = roundHandler.wave;
        inWave = roundHandler.inWave;

        if (inWave)
        {
            //RandomGrow();
        }
    }

    private bool insideBorder(Vector3 pos)
    {
        if (pos.x > maxX || pos.x < minX || pos.y > maxY || pos.y < minY)
        {
            return false;
        }

        return true;
    }

    private List<Vector3> findAllPoints(Vector3 pos)
    {
        List<Vector3> pointList = new List<Vector3>();
        for (int y = minY; y < maxY; y++)
        {
            for (int x = minX; x < maxX; x++)
            {
                Vector3 point = new Vector3(x, y, 0);
                float dist = Vector3.Distance(pos, new Vector3(x, y, 0));
                if (insideBorder(point) && dist < maxRange && dist >= minRange)
                {
                    pointList.Add(point);
                }
            }
        }

        return pointList;
    }

    public void startSpread()
    {
        Invoke("RandomSpawn", 1);
    }

    private void RandomSpawn()
    {
        if (inWave)
        {
            for (int i = 0; i < wave + 1; i++)
            {
                if (pointsList.Count != 0)
                {
                    //get random point from list and remove it
                    int rand = Random.Range(0, pointsList.Count);
                    Vector3 pos = pointsList[rand];
                    pointsList.RemoveAt(rand);

                    int randMask = Random.Range(0, spriteMasks.Length);

                    //initialize and add to list for use later
                    GameObject go = Instantiate(spriteMasks[randMask], pos, Quaternion.identity);
                    maskList.Add(go);

                    //set to random size
                    int randomSize = Random.Range(5, 10);
                    go.transform.localScale = new Vector3(randomSize, randomSize, 1);

                    go.transform.parent = transform;
                }
                else
                {
                    minRange++;
                    maxRange++;

                    pointsList = findAllPoints(this.transform.position);
                }
            }

            Invoke("RandomSpawn", 1);
        }
    }

    private void RandomGrow()
    {
        float scale = maskList[0].transform.localScale.x;
        scale += 0.00083333f;
        maskList[0].transform.localScale = new Vector3(scale * parentXScale, scale * parentYScale, 1);
    }

    private void initialSpawn()
    {
        int randMask = Random.Range(0, spriteMasks.Length);

        //initialize and add to list for use later
        GameObject go = Instantiate(spriteMasks[randMask], this.transform.position, Quaternion.identity);
        maskList.Add(go);

        //set to random size
        int randomSize = Random.Range(5, 10);
        go.transform.localScale = new Vector3(randomSize, randomSize, 1);

        go.transform.parent = transform;
    }
}
