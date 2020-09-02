using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNeighbours : MonoBehaviour
{
    HexagonTileMapGenerator tile;
    Hex hexS;
 
    
    public GameObject nb_Up;
    public GameObject nb_Right_Up;
    public GameObject nb_Right_Down;
    public GameObject nb_Down;
    public GameObject nb_Left_Down;
    public GameObject nb_Left_Up;

    public GameObject[] neighbours;

  

    public int myX;
    public int myZ;
    int width;
    int height;

   
    // Start is called before the first frame update
    void Start()
    {
        tile = GameObject.Find("MapGenerator").GetComponent<HexagonTileMapGenerator>();
        
        neighbours = new GameObject[6];
        hexS = gameObject.GetComponent<Hex>();

        width = tile.width;
        height = tile.height;

        SearchNeighbors();
    }
     void Update()
    {
        myX = hexS.x;
        myZ = hexS.z;
        SearchNeighbors();
        CheckDown();

    }
    //checks dawn and move object if there is no other object in down
   private void CheckDown()
    {
        if(hexS.x>0)
        {
            if (tile.hexArray[hexS.x - 1, hexS.z] == null)
            {   
                tile.hexArray[hexS.x- 1, hexS.z] = this.gameObject;
                tile.hexArray[hexS.x, hexS.z] = null;
                hexS.x--;
                myX = hexS.x;
                myZ = hexS.z;
                transform.name = "( x : " + myX+ ", z : " + myZ + ")";
                float xPos=myX*tile.xOffset;
                if (myZ % 2 == 1)
                {
                    xPos += tile.xOffset / 2;
                }
                transform.position = new Vector3(xPos, 0, myZ * tile.zOffset);
                
            }
        }
    }

    //Search six neighbours in terms of objects array location on 2D array
    public void SearchNeighbors()
    {
        if (myZ % 2 == 0)
        {
            FindEvenNeighbours();
        }
        else
        {
            FindOddNeighbours();
        }
    }

    /*  EVEN Hexagons
     *           (x+1,z)
     *  (x,z+1)          (x,z-1)
     *            (x,z)
     *  (x-1,z+1)        (x-1,z-1)
     *           (x-1,z)
     */
    private void FindEvenNeighbours()
    {
        //up neighbour
        if(hexS.x < height-1)
        {
            nb_Up = tile.hexArray[hexS.x + 1, hexS.z]?.gameObject;
            neighbours[0] = nb_Up ?.gameObject;

        }
        //right up neighbour
        if(hexS.z > 0)
        {
            nb_Right_Up = tile.hexArray[hexS.x, hexS.z - 1]?.gameObject;
            neighbours[1] = nb_Right_Up?.gameObject;
        }
        //right down neighbour
        if(hexS.z > 0 && hexS.x > 0)
        {  
            nb_Right_Down= tile.hexArray[hexS.x - 1, hexS.z - 1]?.gameObject;
            neighbours[2] = nb_Right_Down?.gameObject;
        }
        //down neighbour
        if (hexS.x > 0)
        {
            nb_Down = tile.hexArray[hexS.x - 1, hexS.z]?.gameObject;
            neighbours[3] = nb_Down?.gameObject;
        }
        //left down neighbour
        if (hexS.x > 0 && hexS.z < width-1)
        {
            nb_Left_Down = tile.hexArray[hexS.x - 1, hexS.z + 1]?.gameObject;
            neighbours[4] = nb_Left_Down?.gameObject;
        }
        //left up neighbour
        if (hexS.z < width-1)
        {
           nb_Left_Up = tile.hexArray[hexS.x, hexS.z + 1]?.gameObject;
           neighbours[5] = nb_Left_Up?.gameObject;
        }
    }

    /*  ODD Hexagons
     *            (x+1,z)
     *  (x+1,z+1)          (x+1,z-1)
     *             (x,z)
     *  (x,z+1)            (x,z-1)
     *            (x-1,z)
     */
    private void FindOddNeighbours()
    {
        //up neighbour
        if (hexS.x < height-1)
        {
            nb_Up = tile.hexArray[hexS.x + 1, hexS.z]?.gameObject;
            neighbours[0] = nb_Up?.gameObject;
        }
        //right up neighbour
        if (hexS.x < height-1 && hexS.z > 0)
        {
            nb_Right_Up = tile.hexArray[hexS.x + 1, myZ - 1]?.gameObject;
            neighbours[1] = nb_Right_Up?.gameObject;
        }
        //right down neighbour
        if (hexS.z > 0 )
        {
             nb_Right_Down = tile.hexArray[hexS.x, hexS.z - 1]?.gameObject;
             neighbours[2] = nb_Right_Down?.gameObject;
        }
        //down neighbour
        if (hexS.x > 0)
        {
           nb_Down = tile.hexArray[hexS.x - 1, hexS.z]?.gameObject;
           neighbours[3] = nb_Down?.gameObject;
        }
        //left down neighbour
        if (hexS.z < width-1)
        {
           nb_Left_Down = tile.hexArray[hexS.x, hexS.z + 1]?.gameObject;
           neighbours[4] = nb_Left_Down?.gameObject;
        }
        //left up neighbour
        if (hexS.z < width-1 && hexS.x < height-1)
        {
           nb_Left_Up = tile.hexArray[hexS.x + 1, hexS.z + 1]?.gameObject;
           neighbours[5] = nb_Left_Up?.gameObject;
        }
    }
    public bool MyNeighbourRelationWith(GameObject nb)
    {
        bool relation = false;
        foreach(GameObject n in neighbours)
        {
            if(n!=null)
            {
                if (n.gameObject == nb)
                {
                    relation=true;
                    break;
                }
                else
                    relation=false;
            }
        }
        return relation;
    }
}
