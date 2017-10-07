using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSeed : MonoBehaviour
{

    public float seedMax = 1.65f;
    public float seedMin = 0.47f;
    public GameObject Seed;
    public GameObject player_go;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Score.AddPoint();
            Destroy(this.gameObject);
            SpawnSeed();
        }
    }

    public void SpawnSeed()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Vector3 pos = transform.position;
        pos.y = Random.Range(seedMin, seedMax);
        pos.x = pos.x + 50f;
        Instantiate(gameObject, pos, transform.rotation);
    }

}