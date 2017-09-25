using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooper : MonoBehaviour {

    int numBGPanels = 6;

    void OnTriggerEnter2D (Collider2D collider) 
    {
        float widthofBGObject = ((BoxCollider2D)collider).size.x;

        Vector3 pos = collider.transform.position;

        pos.x += widthofBGObject * numBGPanels - widthofBGObject/2;

        collider.transform.position = pos;
    }
}

