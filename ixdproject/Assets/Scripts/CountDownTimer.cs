using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CountDownTimer : MonoBehaviour {

    private DetectJoints DJ;
    private BirdMovement BM;
    private Rigidbody2D BM_RB;


    float cd_timer = 4.0f;

    // Use this for initialization
    void Start () {
        GetComponent<GUIText>().enabled = false;
        DJ = GameObject.Find("PlayerBird").GetComponent<DetectJoints>();
        BM = GameObject.Find("PlayerBird").GetComponent<BirdMovement>();
        BM_RB = GameObject.Find("PlayerBird").GetComponent<Rigidbody2D>();
        BM.forwardSpeed = 0f;

        BM_RB.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update () {
        cd_timer -= Time.deltaTime;
        GetComponent<GUIText>().text = "" + Mathf.RoundToInt(cd_timer);
        var timer = GetComponent<GUIText>().text;

        if (timer == "3")
        {
            GetComponent<GUIText>().enabled = true;
        }
            else if(timer == "0")
        {
            GetComponent<GUIText>().text = "Start!";
        }
            else if (timer == "-1")
        {
            DJ.GameStarted = true;
            BM_RB.bodyType = RigidbodyType2D.Dynamic;
            BM.forwardSpeed = 8f;
            GetComponent<GUIText>().enabled = false;

        }



        //Debug.Log(cd_timer);

    }
}
