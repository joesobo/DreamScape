using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoundHandler : MonoBehaviour {

    public GameObject enemy;

    private int wave;

    public SideSpawners portalSpawner;
    private List<GameObject> portalList;

    public PlayerController playerController;
    public RoomGenerator roomGenerator;

    private bool breaker = false;

    private bool inWave;

    public int enemiesLeft = 0;
    private int enemiesInRound = 0;
    private int[] waveNumbers = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21};

    public GameObject updatePolygon;

    private void Start()
    {
        portalList = portalSpawner.portalList;
        playerController.transform.position = new Vector3(5, 5, 0);
        inWave = false;
    }

    private void Update()
    {
        //unlock character
        if (roomGenerator.doneGenerating && !breaker)
        {
            AstarPath.active.UpdateGraphs(updatePolygon.GetComponent<PolygonCollider2D>().bounds);

            //unlock player movement
            playerController.unlocker = true;

            breaker = true;
        }

        //start wave
        if (Input.GetKeyDown(KeyCode.Y) && !inWave)
        {
            Debug.Log("Start Wave");
            startRound();
        }
    }

    private void startRound()
    {
        inWave = true;
        enemiesInRound = waveNumbers[wave];

        int enemiesSpawned = 0;

        while(enemiesSpawned < enemiesInRound)
        {
            //spawn enemy at portal
            spawnEnemies();

            enemiesSpawned++;
            enemiesLeft++;

            //wait x number of seconds
        }
    }

    private void spawnEnemies()
    {
        int rand = Random.Range(0, portalList.Count);
        Instantiate(enemy, portalList[rand].transform.position, Quaternion.identity);
    }

    public void enemiesRemaining()
    {
        if(enemiesLeft <= 0)
        {
            Debug.Log("End of Wave");
            inWave = false;
            wave++;
        }
    }
}
