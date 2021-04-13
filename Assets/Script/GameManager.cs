﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public PlayerControl player;

    private float blockSpwanPosition;
    private float blockNumber;
    private void Awake()
    {
        Init();
        StartCoroutine(SpawnBlock());
    }

    private void Init()
    {
        Instantiate(block);
        Instantiate(block, new Vector2(9, Random.Range(-6, 7)), Quaternion.identity);
        blockNumber = 2;
        blockSpwanPosition = 18f;
    }
    private IEnumerator SpawnBlock()
    {
        while (true)
        {
            Instantiate(block, new Vector2(blockSpwanPosition, Random.Range(-6, 7)), Quaternion.identity);
            blockNumber++;
            blockSpwanPosition += 9f;
            yield return new WaitForSeconds(1f);
            if (blockNumber >= 50)
            {
                DestroyBlocks();
               
            }
        }
    }

    private void DestroyBlocks()
    {
        GameObject[] obstacle = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacle.Length - 25; i++)
        {
            Destroy(obstacle[i]);
            blockNumber--;
        }
    }

    public void DestroyAllBlocks()
    {
        GameObject[] obstacle = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var item in obstacle)
        {
            Destroy(item);
        }
        Init();
    }
}
