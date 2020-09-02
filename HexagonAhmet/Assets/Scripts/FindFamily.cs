using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFamily : MonoBehaviour
{
    HexagonTileMapGenerator tile;
    Hex hex;
    GameManager gameManager;
    FindNeighbours neighbour;

    public List<GameObject> family;
    public GameObject[] temp;
    public bool destroyed;

    GameObject family1;
    GameObject family2;
    
    private void Start()
    {
        tile = GameObject.Find("MapGenerator").GetComponent<HexagonTileMapGenerator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hex = GetComponent<Hex>();
        family = new List<GameObject>();
        neighbour = GetComponent<FindNeighbours>();
        Family();
    }
    private void Update()
    {
        Family();
    }
    //find family members(same tag around this)
    void Family()
    {
         temp = neighbour.neighbours;
        foreach (GameObject e in temp)
        {
            if(e!=null)
            {
                if (e.gameObject.tag == this.gameObject.tag)
                {
                    if(family.Find(x=>x.gameObject==e.gameObject)==false)
                    {
                        family.Add(e.gameObject);
                    }
                }
            }
        }
        //when two or more family member around us, call findmatch for findin match with hope of finding match
        if (family.Count >1)
        {
            FindMatch();
        }
    }
    void FindMatch()
    {
        foreach (GameObject g in family)
        {
            foreach (GameObject f in family)
            {
                if(g!=null &&f!=null)
                {
                    if (g.gameObject.GetComponent<FindNeighbours>().MyNeighbourRelationWith(f))
                    {
                        destroyed = true;
                        family1 = g;
                        family2 = f;
                        DestroyObject();
                    }
                }
            }
        }
    }

    //destroy object when called by something
    public void DestroyObject()
    {
        tile.hexArray[hex.x, hex.z] = null;
        Destroy(this.gameObject);
        gameManager.AddCoin();
        gameManager.AddMove();
    }
    //on conditions met and object ready to destroy call two family around us to die :.)
    private void OnDestroy()
    {
        if (family1 != null)
        {
            family1.gameObject.GetComponent<FindFamily>().family.RemoveAll(x => x.gameObject == transform.gameObject);
            family1.GetComponent<FindFamily>().DestroyObject();
        }
       
        if (family2 != null)
        {
            family2.gameObject.GetComponent<FindFamily>().family.RemoveAll(x => x.gameObject == transform.gameObject);
            family2.GetComponent<FindFamily>().DestroyObject();
        }

    }

}
