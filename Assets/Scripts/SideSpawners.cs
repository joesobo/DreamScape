using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawners : MonoBehaviour {

    public int maxSpawners = 1;
    private int numSpawners = 0;

    public GameObject portalPrefab;

    public List<GameObject> portalList;

    private int first = -1;
    private int second = -1;

    public void addSpawners(List<GameObject> rooms)
    {
        while (numSpawners < maxSpawners)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                //1 in 5 chance
                if (numSpawners < maxSpawners && Random.Range(0, 4) == 0)
                {
                    //left side
                    if ((i-1) % 5 == 0 && i != first && i != second)
                    {
                        portalList.Add(Instantiate(portalPrefab, new Vector3(-36.75f, rooms[i].transform.position.y, 0), Quaternion.identity));
                        numSpawners++;
                        if(first == -1)
                        {
                            first = i;
                        }else if(second == -1)
                        {
                            second = i;
                        }

                        if(numSpawners >= maxSpawners)
                        {
                            break;
                        }
                    }
                    //right side
                    else if (i % 5 == 0 && i != first && i != second)
                    {
                        portalList.Add(Instantiate(portalPrefab, new Vector3(36.75f, rooms[i].transform.position.y, 0), Quaternion.identity));
                        numSpawners++;
                        if (first == -1)
                        {
                            first = i;
                        }
                        else if (second == -1)
                        {
                            second = i;
                        }

                        if (numSpawners >= maxSpawners)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

}
