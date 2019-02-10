//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class levelGeneration : MonoBehaviour {

//    public Transform[] pos;
//    public GameObject[] rooms;

//    private int direction;
//    public float offset = 10;

//    private float timeBtwRoom;
//    public float startTimeBtwRoom = 0.25f;

//    public float minX;
//    public float maxX;
//    public float minY;

//    public bool stopGen = false;
//    public bool doneGen = false;
    
//    public LayerMask roomMask;

//    private int downCounter;

//    private List<GameObject> roomObj;

//    public PlayerController playerController;
//    public SideSpawners portalSpawner;

//    public int enemyCount;

//    private void Awake()
//    {
//        roomObj = new List<GameObject>();
//        int randStartingPos = Random.Range(0, 4);
//        transform.position = pos[randStartingPos].position;
//        roomObj.Add(Instantiate(rooms[0], transform.position, Quaternion.identity));

//        direction = Random.Range(1, 6);
//    }

//    private void Update()
//    {
//        //spawn main path
//        if(timeBtwRoom <= 0 && !stopGen)
//        {
//            Move();
//            timeBtwRoom = startTimeBtwRoom;
//        }
//        else
//        {
//            timeBtwRoom -= Time.deltaTime;
//        }

//        //spawn all other rooms
//        if (stopGen && !doneGen)
//        {
//            for (int i = 0; i < pos.Length; i++) {
//                Collider2D roomDetection = Physics2D.OverlapCircle(pos[i].position, 1, roomMask);
//                if (roomDetection == null)
//                {
//                    int rand = Random.Range(0, rooms.Length);
//                    roomObj.Add(Instantiate(rooms[rand], pos[i].position, Quaternion.identity));
//                }
//            }

//            //add portals on sides
//            portalSpawner.addSpawners(roomObj);

//            //spawn in enemies
//            enemyCount = spawner(roomObj);

//            //unlock player movement
//            playerController.unlock();

//            doneGen = true;
//        }
//    }

//    private void Move()
//    {
//        //move right (LR or LRB or LRT or all)
//        if (direction == 1 || direction == 2)
//        {
//            downCounter = 0;
//            if(transform.position.x < maxX)
//            {
//                Vector2 newPos = new Vector2(transform.position.x + offset, transform.position.y);
//                transform.position = newPos;

//                //pick random room
//                int rand = Random.Range(0, rooms.Length);
//                roomObj.Add(Instantiate(rooms[rand], transform.position, Quaternion.identity));

//                direction = Random.Range(1, 6);
//                if(direction == 3)
//                {
//                    direction = 2;
//                }
//                else if(direction == 4)
//                {
//                    direction = 5;
//                }
//            }
//            else
//            {
//                direction = 5;
//            }
            
//        }
//        //move left (LR or LRB or LRT or all)
//        else if (direction == 3 || direction == 4)
//        {
//            downCounter = 0;
//            if(transform.position.x > minX)
//            {
//                Vector2 newPos = new Vector2(transform.position.x - offset, transform.position.y);
//                transform.position = newPos;

//                //pick random room
//                int rand = Random.Range(0, rooms.Length);
//                roomObj.Add(Instantiate(rooms[rand], transform.position, Quaternion.identity));

//                direction = Random.Range(3, 6);
//            }
//            else
//            {
//                direction = 5;
//            }
           
//        }
//        //move down (LRT or ALL)
//        else if (direction == 5)
//        {
//            downCounter++;
            
//            if(transform.position.y > minY)
//            {
//                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomMask);
//                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
//                {
//                    if(downCounter >= 2)
//                    {
//                        roomDetection.GetComponent<RoomType>().RoomDestruction();
//                        roomObj.Add(Instantiate(rooms[3], transform.position, Quaternion.identity));
//                    }
//                    else
//                    {
//                        roomDetection.GetComponent<RoomType>().RoomDestruction();

//                        int randBottomRoom = Random.Range(1, 4);
//                        if (randBottomRoom == 2)
//                        {
//                            randBottomRoom = 1;
//                        }

//                        roomObj.Add(Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity));
//                    }
//                }

//                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - offset);
//                transform.position = newPos;

//                int rand = Random.Range(2, 4);
//                roomObj.Add(Instantiate(rooms[rand], transform.position, Quaternion.identity));

//                direction = Random.Range(1, 6);
//            }
//            else
//            {
//                stopGen = true;
//            }
//        }
//    }

//    private int spawner(List<GameObject> obj)
//    {
//        SpawnEnemy spawnEnemy;
//        int count = 0;
//        foreach (GameObject g in obj)
//        {
//            spawnEnemy = g.GetComponentInChildren<SpawnEnemy>();
//            if(spawnEnemy != null)
//            {
//                spawnEnemy.spawn();
//                count++;
//            }
//        }
//        return count;
//    }
//}
