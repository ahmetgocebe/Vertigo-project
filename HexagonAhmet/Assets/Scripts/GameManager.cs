using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    HexagonTileMapGenerator tile;
    UIManager uIManager;
    public int coinCounter=0;
    public int coinPerHex = 5;
    int moveCounter = 0;
    public int bombPerCoin = 1000;
    public GameObject bomb;
    public int bombTimer=5;
    public bool isBombActive = false;
    int randX;
    int randZ;
    private void Start()
    {
        uIManager = GetComponent<UIManager>();
        tile = GameObject.Find("MapGenerator").GetComponent<HexagonTileMapGenerator>();
        moveCounter = 0;
    }
    private void Update()
    {
        uIManager.scoreValue.text = coinCounter.ToString();
        if (coinCounter % bombPerCoin == 0)
        {
            CreateBomb();
        }
        if(isBombActive==true)
        {
            CheckBomb();
            IsBombDisabled();
        }
    }

    private void CreateBomb()
    {
        if(isBombActive==false)
        {
            randX = UnityEngine.Random.Range(0, tile.height);
            randZ = UnityEngine.Random.Range(0, tile.width);
            Instantiate(bomb, new Vector3(randX, randZ), Quaternion.identity,transform);
            ResetMove();
            isBombActive = true;
        }
    }

    void CheckBomb()
    {
        if(bombTimer==0)
        {
            BombExploded();
        }
    }
    void IsBombDisabled()
    {
        if(tile.hexArray[randX,randZ]==null)
        {
            isBombActive = false;
        }
    }
    void BombExploded()
    {
        SceneManager.LoadScene("GameOverScene",LoadSceneMode.Single);
    }

    public void AddCoin()
    {
        coinCounter += coinPerHex;
    }
    public void AddMove()
    {
        if(isBombActive)
        {
            bombTimer--;
        }
        moveCounter++;
    }
    public void ResetMove()
    {
        moveCounter = 0;
    }
    public int GetMove()
    {
        return moveCounter;
    }
}
