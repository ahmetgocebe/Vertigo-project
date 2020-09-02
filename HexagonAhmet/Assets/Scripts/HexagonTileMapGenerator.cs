using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTileMapGenerator : MonoBehaviour
{
    public GameObject []HexPrefab;
    public GameObject[,] hexArray;
    

    public int width;
    public int height;
    public float xOffset = 1.1f; 
    public float zOffset = 0.95f;


    void Awake()
    {
        hexArray = new GameObject[height, width];
        MakeHexTile();
       
    }
    void MakeHexTile()
    {
        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < height; x++)
            {
                float xPos = x * xOffset;

                if (z % 2 == 1)
                {
                    xPos += xOffset / 2;
                }
                
                //instantiate and add object to 2D array
                hexArray[x, z] = Instantiate(HexPrefab[UnityEngine.Random.Range(0, HexPrefab.Length)], 
                    new Vector3(xPos, 0, z * zOffset), Quaternion.identity, this.transform);

                //set name according to its position in array
                hexArray[x, z].name = "( x : " + x + ", z : " + z + ")";
               
                //passing position info to object
                hexArray[x, z].GetComponent<Hex>().x = x;
                hexArray[x, z].GetComponent<Hex>().z = z;
            }
        }
    }
   
    private void Update()
    {
      RESPAWN();
    }
    //respawn hexagons  
    private void RESPAWN()
    {
        int j = height - 1;
        for (int i = 0; i < width; i++)
        {
            if(hexArray[j,i]==null)
            {
                float jPos = j * xOffset;

                if (i % 2 == 1)
                {
                    jPos += xOffset / 2;
                }
                //instantiate and add object to 2D array
                hexArray[j, i] = Instantiate(HexPrefab[UnityEngine.Random.Range(0, HexPrefab.Length)],
                    new Vector3(jPos, 0, i * zOffset), Quaternion.identity, this.transform);

                //set name according to its position in array
                hexArray[j, i].name = "( x : " + j + ", z : " + i + ")";

                //passing position info to object
                hexArray[j, i].GetComponent<Hex>().x = j;
                hexArray[j, i].GetComponent<Hex>().z = i;
                
            }
        }
    }
}
