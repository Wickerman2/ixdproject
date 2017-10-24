using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour {

    bool playAudio = false;
    public AudioClip countdownSound;
    public AudioSource audioSource;

    private DetectFlap detectFlap;
    private Rigidbody2D birdRigidbody;

    float cd_timer = 3.5f;

    // Use this for initialization
    void Start () {

        audioSource.clip = countdownSound;

        detectFlap = GameObject.Find("PlayerBird").GetComponent<DetectFlap>();
        birdRigidbody = GameObject.Find("PlayerBird").GetComponent<Rigidbody2D>();
        BirdMovement.forwardSpeed = 0f;
        birdRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update () {
        cd_timer -= Time.deltaTime;
        GetComponent<GUIText>().text = "" + Mathf.RoundToInt(cd_timer);
        var timer = GetComponent<GUIText>().text;

        if (timer == "3")
        {
            GetComponent<GUIText>().enabled = true;
            if (!playAudio)
            {
                audioSource.PlayOneShot(countdownSound);
                playAudio = true;
            }
        }
            else if(timer == "0")
        {
            GetComponent<GUIText>().text = "Start!";
        }
        else if (timer == "-1")
        {
            detectFlap.GameStarted = true;
            birdRigidbody.bodyType = RigidbodyType2D.Dynamic;
            BirdMovement.forwardSpeed = 6f;
            GetComponent<GUIText>().enabled = false;
        }
    }
}
