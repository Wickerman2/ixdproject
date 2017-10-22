using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour
{

    int numBGPanels = 3;
    Transform player;

    public float berryMax = 1.65f;
    public float berryMin = 0.47f;

    void Start()
    {
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
        player = player_go.transform;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        float widthOfBGObject = ((BoxCollider2D)collider).size.x;

        Vector3 pos = collider.transform.position;

        pos.x += widthOfBGObject * numBGPanels;

        if (collider.tag == "Berry")
        {
            pos.y = Random.Range(berryMin, berryMax);
            pos.x = player.transform.position.x + 18.0f;
        }

        collider.transform.position = pos;

    }
}
