using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBerry : MonoBehaviour
{

    public float seedMax = 1.65f;
    public float seedMin = 0.47f;
    public GameObject Seed;
    public AudioSource audioSource;
    public AudioClip pickupAudio;

    // Use this for initialization
    void Start()
    {
        audioSource.clip = pickupAudio;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision) // Every time the bird picks up a berry. 
    {
        if (collision.CompareTag("Player"))
        {
            Score.AddPoint();
            Destroy(this.gameObject);
            SpawnBerry();
            audioSource.PlayOneShot(pickupAudio);
        }
    }

    public void SpawnBerry() 
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Vector3 pos = transform.position;
        pos.y = Random.Range(seedMin, seedMax);
        pos.x = pos.x + 40.0f;
        Instantiate(gameObject, pos, transform.rotation);
    }

}