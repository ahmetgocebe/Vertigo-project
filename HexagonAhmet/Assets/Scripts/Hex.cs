using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    GameObject ManagerGame;
    InputManager managerInput;

    [Header("object Position in array")]
    public int x;
    public int z;

    
    [Header("neighbours and indicator")]
    public GameObject[] nb;
    public GameObject Indicator;
    GameObject indicatorTemp;
    [Header("check this object selected")]
    public bool amISelected = false;
    private void Start()
    {
        nb = new GameObject[6];
        ManagerGame = GameObject.Find("GameManager");
        managerInput = ManagerGame.GetComponent<InputManager>();
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
    private void Update()
    {
        nb[0] = GetComponent<FindNeighbours>()?.nb_Up;
        nb[1] = GetComponent<FindNeighbours>()?.nb_Right_Up;
        nb[2] = GetComponent<FindNeighbours>()?.nb_Right_Down;
        nb[3] = GetComponent<FindNeighbours>()?.nb_Down;
        nb[4] = GetComponent<FindNeighbours>()?.nb_Left_Down;
        nb[5] = GetComponent<FindNeighbours>()?.nb_Left_Up;
        
        //check if object selected by player
        AmISelected();
    }


    //place indicator object for indicate : "this object have been selected by player".
    void AmISelected()
    {
        if (managerInput.host==this.gameObject || managerInput.guest1==this.gameObject || managerInput.guest2==this.gameObject)
        {
            if (indicatorTemp==null)
            {
                indicatorTemp=Instantiate(Indicator,transform.position, Quaternion.identity,transform);
                amISelected = false;
            }
        }
        else
        {
            if(indicatorTemp!=null)
            {
                Destroy(indicatorTemp);
            }
        }
    }
}
