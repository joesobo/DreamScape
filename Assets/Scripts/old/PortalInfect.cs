//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PortalInfect : MonoBehaviour {

//    public GameObject spriteMask;

//    public int maxX;
//    public int minX;
//    public int maxY;
//    public int minY;

//    public float minTime = 1;
//    public float maxTime = 5;

//    public int range = 5;

//    private List<GameObject> maskList = new List<GameObject>();

//    private List<Vector3> pointsList;

//    private void Start()
//    {
//        pointsList = findAllPoints(this.transform.position);

//        Invoke("RandomSpawn", 0.5f);
//    }

//    private void RandomSpawn()
//    {
//        float randTime = Random.Range(minTime, maxTime);

//        if (pointsList.Count != 0)
//        {
//            int rand = Random.Range(0, pointsList.Count);
//            Vector3 pos = pointsList[rand];
//            pointsList.RemoveAt(rand);

//            GameObject go = Instantiate(spriteMask, pos, Quaternion.identity);
//            maskList.Add(go);
//            int randomSize = Random.Range(5, 25);
//            go.transform.localScale = new Vector3(randomSize, randomSize, 1);

//            go.transform.parent = transform;

//            Invoke("RandomSpawn", randTime);
//        }
//        else
//        {
//            Debug.Log("End Spawn");
//        }
//    }

//    private bool insideBorder(Vector3 pos)
//    {
//        if (pos.x > maxX || pos.x < minX || pos.y > maxY || pos.y < minY){
//            return false;
//        }

//        return true;
//    }

//    private List<Vector3> findAllPoints(Vector3 pos)
//    {
//        List<Vector3> pointList = new List<Vector3>();
//        for (int x = minX; x < maxX; x++)
//        {
//            for (int y = minY; y < maxY; y++)
//            {
//                Vector3 point = new Vector3(x, y, 0);
//                if(insideBorder(point) && Vector3.Distance(pos, new Vector3(x, y, 0)) < range)
//                {
//                    pointList.Add(point);
//                }
//            }
//        }

//        return pointList;
//    }
//}
