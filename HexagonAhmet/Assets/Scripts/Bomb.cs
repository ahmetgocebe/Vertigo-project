using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public TextMesh txt;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TextMesh>();
        
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    { 
        txt.text = manager.bomb.ToString();
    }
}
