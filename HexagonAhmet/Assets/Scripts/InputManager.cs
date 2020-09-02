using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager : MonoBehaviour
{

    public GameObject guest1;
    public GameObject guest2;
    public GameObject host;
    public GameObject[] neighbours;    
    HexagonTileMapGenerator tile;
    GameObject dragObject;
    Vector3 vecSpin;
    private void Start()
    {
         dragObject = new GameObject();
        dragObject.SetActive(false);
        tile = GameObject.Find("MapGenerator").GetComponent<HexagonTileMapGenerator>();
    }
    // Update is called once per frame
    private void Update()
    {

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Raycast(Input.GetTouch(0).position);
                }
                if(Input.GetTouch(0).phase==TouchPhase.Moved)
                {
                    if(Input.GetTouch(0).deltaPosition.x>1 || Input.GetTouch(0).deltaPosition.y > 1)
                    {
                        Drag();
                    }
                    
                }
            }  
        }
        
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Raycast(Input.mousePosition);
            }
            if (Input.GetKey(KeyCode.Q))
            {
               Drag();
            }
        }
    }
    private void Raycast(Vector3 pos)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        //raycastin screen position to world position 
        if (Physics.Raycast(ray, out hit, 100f))
        {
                host = hit.collider.GetComponentInParent<Hex>().GetGameObject();
                GetTwoNeighbours(hit.collider.gameObject.GetComponentInParent<FindNeighbours>());
        }
    }
    //when player drag spin objects
    private void Drag()
    {
       // created a new object to proper spinning move
        dragObject.gameObject.SetActive(true);

        if (guest1 != null && guest2 != null && host != null)
        {
            float xCenter = (host.transform.position.x + guest1.transform.position.x + guest2.transform.position.x) / 3;
            float zCenter = (host.transform.position.z + guest1.transform.position.z + guest2.transform.position.z) / 3; 
            vecSpin = new Vector3(xCenter, 0, zCenter);
            
            //spin
            Spin(host, guest1, guest2);
            
        }
    }
    ////change three objects array order clock wise
    //private void ChangeThreeObjectsInArray(GameObject host,GameObject guest1,GameObject guest2)
    //{
    //    GameObject tempA=host;
    //    int aX = host.GetComponent<Hex>().x;
    //    int aZ = host.GetComponent<Hex>().z;
    //    GameObject tempB=guest1;
    //    int bX = guest1.GetComponent<Hex>().x;
    //    int bZ = guest1.GetComponent<Hex>().z;
    //    GameObject tempC=guest2;
    //    int cX = guest2.GetComponent<Hex>().x;
    //    int cZ = guest2.GetComponent<Hex>().z;
        
    //    tile.hexArray[bX,bZ] = host;
    //    host.GetComponent<Hex>().x = bX;
    //    host.GetComponent<Hex>().z = bZ;

    //    tile.hexArray[cX, cZ] = tempB;
    //    tempB.GetComponent<Hex>().x = cX;
    //    tempB.GetComponent<Hex>().z = cZ;

    //    tile.hexArray[aX, aZ] = tempC;
    //    tempC.GetComponent<Hex>().x = aX;
    //    tempC.GetComponent<Hex>().z = aZ;

    //}
    private void Spin(GameObject host,GameObject guest1,GameObject guest2)
    {
        Vector3 rotation = Vector3.up * 120f;
        

        if (host!=null && guest1!=null && guest2!=null)
        {
            GameObject tempA = host;
            int aX = host.GetComponent<Hex>().x;
            int aZ = host.GetComponent<Hex>().z;
            GameObject tempB = guest1;
            int bX = guest1.GetComponent<Hex>().x;
            int bZ = guest1.GetComponent<Hex>().z;
            GameObject tempC = guest2;
            int cX = guest2.GetComponent<Hex>().x;
            int cZ = guest2.GetComponent<Hex>().z;

            tile.hexArray[bX, bZ] = host;
            host.GetComponent<Hex>().x = bX;
            host.GetComponent<Hex>().z = bZ;
            host.transform.RotateAround(vecSpin, Vector3.up, 120f);

            tile.hexArray[cX, cZ] = tempB;
            tempB.GetComponent<Hex>().x = cX;
            tempB.GetComponent<Hex>().z = cZ;
            guest1.transform.RotateAround(vecSpin, Vector3.up, 120f);

            tile.hexArray[aX, aZ] = tempC;
            tempC.GetComponent<Hex>().x = aX;
            tempC.GetComponent<Hex>().z = aZ;
            guest2.transform.RotateAround(vecSpin, Vector3.up, 120f);
        }
    
    }

    //get two neighbours of selected hexagon
    private void GetTwoNeighbours(FindNeighbours host)
    {
        guest1 = null;
        guest2 = null;
        neighbours = host.neighbours;
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null)
            {
                guest1 = host.neighbours[i].gameObject;
                guest1.GetComponent<Hex>().amISelected = true;
                break;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null)
            {
                if (neighbours[i].gameObject.GetComponent<FindNeighbours>().MyNeighbourRelationWith(guest1) == true && guest1!=null)
                {
                    guest2 = neighbours[i].gameObject;
                    guest2.GetComponent<Hex>().amISelected = true;
                    host.GetComponent<Hex>().amISelected = true;
                    break;
                }
            }
        }
    }
}
