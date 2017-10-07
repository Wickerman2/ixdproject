﻿using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour
{

    int numBGPanels = 3;
    Transform player;

    public float seedMax = 1.65f;
    public float seedMin = 0.47f;

    void Start()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
        player = player_go.transform;

        foreach (GameObject pipe in pipes)
        {
            Vector3 pos = pipe.transform.position;
            pos.y = Random.Range(seedMin, seedMax);
            pipe.transform.position = pos;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        float widthOfBGObject = ((BoxCollider2D)collider).size.x;

        Vector3 pos = collider.transform.position;

        pos.x += widthOfBGObject * numBGPanels;

        if (collider.tag == "Seed")
        {
            pos.y = Random.Range(seedMin, seedMax);
            pos.x = player.transform.position.x + 22.0f;
        }

        collider.transform.position = pos;

    }
}