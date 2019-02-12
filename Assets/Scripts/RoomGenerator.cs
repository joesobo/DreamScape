using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct indexArray
{
    [SerializeField] public int[] num;
}

[System.Serializable]
public struct accessArray
{
    [SerializeField] public int[] num;
}

public class RoomGenerator : MonoBehaviour {

    public GameObject parent;

    public GameObject[] prefabArray;

    //points to spawn rooms in
    public GameObject[] pointArray;

    //index of room in value roomNum
    private indexArray[] indexArray;

    //accessability of rooms (1)
    private accessArray[] accessArray;

    private int currentX;
    private int currentY;

    private int direction;
    //private int offset = 15;

    private enum dir { R, L, D, U }
    private enum roomType { R, L, U, D, RL, RU, RD, LU, LD, UD, RLU, RLD, LUD, UDR, ALL }
    private List<int> roomNum = new List<int> {2, 3, 5, 7, 6, 10, 14, 15, 21, 35, 30, 70, 42, 105, 210};

    private bool firstGen;
    public bool doneGenerating = false;

    private List<GameObject> roomObj;

    public SideSpawners portalSpawner;

    private void Start()
    {
        roomObj = new List<GameObject>();

        //generates rooms until all accessible (TEMP FIX)
        generate();
        while (!allOne())
        {
            generate();
        }

        //spawn in rooms
        spawnRooms();

        //add portals on sides
        portalSpawner.addSpawners(roomObj);

        doneGenerating = true;
    }

    private void generate()
    {
        firstGen = false;

        indexArray = new indexArray[5];
        accessArray = new accessArray[5];

        for (int i = 0; i < 5; i++)
        {
            //pointArray[i].num = new GameObject[5];
            indexArray[i].num = new int[5] { 1, 1, 1, 1, 1 };
            accessArray[i].num = new int[5];
        }

        currentY = 0;
        currentX = Random.Range(0, 5);
        accessArray[currentY].num[currentX] = 1;

        direction = Random.Range(0, 3);

        //generate initial random path
        accessPath();

        initialRooms();

        //generate random rooms and assign values
        randRooms();

        //add connections to rooms (all 1)
        addConnections();

        //set middle room to ALL
        indexArray[2].num[2] = 14;
    }

    //generates main path in access
    private void accessPath()
    {
        while (!firstGen)
        {
            //moving right (has a left opening)
            if(direction == (int)dir.L)
            {
                if(currentX < 4)
                {
                    currentX++;
                    accessArray[currentY].num[currentX] = 1;

                    direction = Random.Range(0, 3);
                    if(direction == (int)dir.R)
                    {
                        direction = (int)dir.L;
                    }
                }
                else
                {
                    direction = (int)dir.D;
                }
            }
            //moving left (has a right opening)
            else if(direction == (int)dir.R)
            {
                if(currentX > 0)
                {
                    currentX--;
                    accessArray[currentY].num[currentX] = 1;

                    direction = Random.Range(0, 3);
                    if (direction == (int)dir.L)
                    {
                        direction = (int)dir.R;
                    }
                }
                else
                {
                    direction = (int)dir.D;
                }
            }
            //moving down (has an up opening)
            else if(direction == (int)dir.D)
            {
                if(currentY < 4)
                {
                    currentY++;
                    accessArray[currentY].num[currentX] = 1;

                    direction = Random.Range(0, 3);
                }
                else
                {
                    firstGen = true;
                }
            }
        }
    }

    //generates initial rooms (connected)
    private void initialRooms()
    {
        for (int x = 0; x <= 4; x++)
        {
            for (int y = 0; y <= 4; y++)
            {
                if (accessArray[y].num[x] == 1)
                {
                    //find nearby rooms
                    int count = checkSurroundingRooms(x, y);
                    int index = roomNum.FindIndex(d => d == count);
                    indexArray[y].num[x] = index;
                }
            }
        }
    }

    //sets initial rooms to value
    private int checkSurroundingRooms(int x, int y)
    {
        int count = 1;
        //check left
        if(x > 0)
        {
            if (accessArray[y].num[x - 1] == 1)
            {
                count *= 3;
            }
        }
        //check right
        if(x < 4)
        {
            if(accessArray[y].num[x + 1] == 1)
            {
                count *= 2;
            }
        }
        //check up
        if (y > 0)
        {
            if (accessArray[y - 1].num[x] == 1)
            {
                count *= 5;
            }
        }
        //check down
        if (y < 4)
        {
            if (accessArray[y + 1].num[x] == 1)
            {
                count *= 7;
            }
        }

        return count;
    }

    //loops through accessArray looking for 0
    //if 0 set value array at position to random room
    private void randRooms()
    {
        int score = 1;
        while (zeroExists())
        {
            for (int y = 0; y <= 4; y++)
            {
                for (int x = 0; x <= 4; x++)
                {
                    if (accessArray[y].num[x] == score)
                    {
                        setNearby(x, y, score);
                    }
                }
            }
            score++;
        }
    }

    //checks 4 rooms around point and assigns score to any 0's
    private void setNearby(int x, int y, int score)
    {
        //check left
        if (x > 0)
        {
            if (accessArray[y].num[x - 1] == 0)
            {
                accessArray[y].num[x - 1] = score + 1;
                indexArray[y].num[x - 1] = Random.Range(0, 15);
                //connect(x, y, x - 1, y);
            }
        }
        //check right
        if (x < 4)
        {
            if (accessArray[y].num[x + 1] == 0)
            {
                accessArray[y].num[x + 1] = score + 1;
                indexArray[y].num[x + 1] = Random.Range(0, 15);
                //connect(x, y, x + 1, y);
            }
        }
        //check up
        if (y > 0)
        {
            if (accessArray[y - 1].num[x] == 0)
            {
                accessArray[y - 1].num[x] = score + 1;
                indexArray[y - 1].num[x] = Random.Range(0, 15);
                //connect(x, y, x, y - 1);
            }
        }
        //check down
        if (y < 4)
        {
            if (accessArray[y + 1].num[x] == 0)
            {
                accessArray[y + 1].num[x] = score + 1;
                indexArray[y + 1].num[x] = Random.Range(0, 15);
                //connect(x, y, x, y + 1);
            }
        }
    }

    //check if 0 still exists in access list
    private bool zeroExists()
    {
        for (int x = 0; x <= 4; x++)
        {
            for (int y = 0; y <= 4; y++)
            {
                if (accessArray[y].num[x] == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //loops through a until all in access list is 1
    private void addConnections()
    {
        int num = highestNum() + 1;
        for (int i = 2; i < num; i++)
        {
            for (int y = 0; y <= 4; y++)
            {
                for (int x = 0; x <= 4; x++)
                {
                    if (accessArray[y].num[x] == i)
                    {
                        connectAround(x, y);
                    }
                }
            }
        }
    }

    //finds number of times to loop
    private int highestNum()
    {
        int num = 0;
        for (int x = 0; x <= 4; x++)
        {
            for (int y = 0; y <= 4; y++)
            {
                if (accessArray[y].num[x] > num)
                {
                    num = accessArray[y].num[x];
                }
            }
        }

        return num;
    }

    //checks 2 adjecent rooms for connection
    //if not adds one to both rooms
    //checks left then right then down then up
    private void connect(int x1, int y1, int x2, int y2)
    {
        int val;

        int checkIndex = indexArray[y2].num[x2];
        int curIndex = indexArray[y1].num[x1];
        //check left
        if(x2 < x1 && x1 > 0)
        {
            //if value of left room doesnt have right opening
            if(roomNum[checkIndex] % 2 != 0)
            {
                //add right opening
                val = roomNum[checkIndex] * 2;
                indexArray[y2].num[x2] = roomNum.FindIndex(d => d == val);
            }
            //if value of current room doesnt have left opening
            if (roomNum[curIndex] % 3 != 0)
            {
                //add left opening
                val = roomNum[curIndex] * 3;
                indexArray[y1].num[x1] = roomNum.FindIndex(d => d == val);
            }
            accessArray[y1].num[x1] = 1;
        }
        //check right
        else if (x2 > x1 && x1 < 4)
        {
            if (roomNum[checkIndex] % 3 != 0)
            {
                val = roomNum[checkIndex] * 3;
                indexArray[y2].num[x2] = roomNum.FindIndex(d => d == val);
            }
            if (roomNum[curIndex] % 2 != 0)
            {
                val = roomNum[curIndex] * 2;
                indexArray[y1].num[x1] = roomNum.FindIndex(d => d == val);
            }
            accessArray[y1].num[x1] = 1;
        }
        //check down
        else if (y2 > y1 && y1 > 0)
        {
            if (roomNum[checkIndex] % 5 != 0)
            {
                val = roomNum[checkIndex] * 5;
                indexArray[y2].num[x2] = roomNum.FindIndex(d => d == val);
            }
            if (roomNum[curIndex] % 7 != 0)
            {
                val = roomNum[curIndex] * 7;
                indexArray[y1].num[x1] = roomNum.FindIndex(d => d == val);
            }
            accessArray[y1].num[x1] = 1;
        }
        //check up
        else if (y2 < y1 && y1 < 4)
        {
            if (roomNum[checkIndex] % 7 != 0)
            {
                val = roomNum[checkIndex] * 7;
                indexArray[y2].num[x2] = roomNum.FindIndex(d => d == val);
            }
            if (roomNum[curIndex] % 5 != 0)
            {
                val = roomNum[curIndex] * 5;
                indexArray[y1].num[x1] = roomNum.FindIndex(d => d == val);
            }
            accessArray[y1].num[x1] = 1;
        }
    }

    //checks rooms around for access to main path (1)
    //if found connect the rooms
    private void connectAround(int x, int y)
    {
        //check left
        if(x > 0 && accessArray[y].num[x-1] == 1)
        {
            connect(x, y, x - 1, y);
        }

        //check right
        else if(x < 4 && accessArray[y].num[x+1] == 1)
        {
            connect(x, y, x + 1, y);
        }

        //check up
        else if(y > 0 && accessArray[y-1].num[x] == 1)
        {
            connect(x, y, x, y - 1);
        }

        //check down
        else if(y < 4 && accessArray[y+1].num[x] == 1)
        {
            connect(x, y, x, y + 1);
        }
    }

    //initializes rooms at positions
    private void spawnRooms()
    {
        for(int y = 0; y <= 4; y++)
        {
            for(int x = 0; x <= 4; x++)
            {
                Vector3 pos = pointArray[(y*5) + x].transform.position;
                int index = indexArray[y].num[x];
                GameObject go = Instantiate(prefabArray[index], pos, Quaternion.identity);
                go.transform.SetParent(parent.transform);
                roomObj.Add(go);
            }
        }
    }

    //returns true if all rooms are accessable
    private bool allOne()
    {
        for(int y = 0; y < 5; y++)
        {
            for(int x = 0; x < 5; x++)
            {
                if(accessArray[y].num[x] != 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
