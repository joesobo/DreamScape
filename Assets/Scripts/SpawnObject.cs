using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject[] objects;
    public GameObject[] blackObjects;

    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;


        if (blackObjects.Length != 0)
        {
            int rand2 = Random.Range(0, blackObjects.Length);
            GameObject blackInstance = Instantiate(blackObjects[rand2], transform.position, Quaternion.identity);
            blackInstance.transform.parent = transform;
        }
        
       
    }
}
