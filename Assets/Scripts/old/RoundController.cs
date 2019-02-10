//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RoundController : MonoBehaviour {
//    public GameObject spawner;
//    public int waveNumber = 0;
//    public int enemiesLeft = 0;
//    private int enemiesSpawned = 0;

//    public SideSpawners portalSpawner;
//    private List<GameObject> portalList;

//    private List<int> enemiesPerWave = new List<int>{3, 5, 7, 9, 12};

//    public levelGeneration levelGen;

//    private void Start()
//    {
//        portalList = portalSpawner.portalList;
//        enemiesLeft = levelGen.enemyCount;
//    }

//    private void Update()
//    {
//        if (levelGen.doneGen)
//        {
//            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;

//            //start round with space, if no enemies left (AKA after all enemies are dead, you can start another round)
//            if (Input.GetKeyDown(KeyCode.Space) && enemiesLeft <= 0)
//            {
//                //startRound();
//            }
//        }
//    }

//    //void startRound()
//    //{
//    //    enemiesSpawned = 0;
//    //    SpawnEnemy spawn = null;

//    //    int i = 0;
//    //    while (enemiesSpawned < enemiesPerWave[waveNumber])
//    //    {
//    //        if (i % 3 == 0)
//    //        {
//    //            StartCoroutine(spawnRound(spawn, 0));
//    //            enemiesSpawned++;
//    //            i++;
//    //        }
//    //        else if ((i - 1) % 3 == 0)
//    //        {
//    //            StartCoroutine(spawnRound(spawn, 1));
//    //            enemiesSpawned++;
//    //            i++;
//    //        }
//    //        else if ((i - 2) % 3 == 0)
//    //        {
//    //            StartCoroutine(spawnRound(spawn, 2));
//    //            enemiesSpawned++;
//    //            i++;
//    //        }
//    //    }
//    //    waveNumber++;
//    //}

//    //IEnumerator spawnRound  (SpawnEnemy spawn, int i)
//    //{
//    //    yield return new WaitForSeconds(.5f);
//    //    spawnEnemy(spawn, i);
//    //}

//    //void spawnEnemy(SpawnEnemy spawn, int i)
//    //{
//    //    spawn = Instantiate(spawner, portalList[i].transform.position, Quaternion.identity).GetComponent<SpawnEnemy>();
//    //    spawn.spawn();
//    //}
//}
