using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DetectFlap : MonoBehaviour
{

    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;

    public JointType HandLeft;
    public JointType HandRight;
    float timer;
    float flapthreshold;
    float flapRate = 0.75f;
    float nextFlap;
    int numberOfFlaps = 0;

    GameObject LeftHandCube;
    GameObject RightHandCube;
    Transform leftHandTransform;
    Transform rightHandTransform;
    int bodyCount;
    private Body[] bodies;
    public Body body;

    float previousLeftHandPositionY = 0.0f;
    float previousRightHandPositionY = 0.0f;
    float currentLeftHandPositionY = 0.0f;
    float currentRightHandPositionY = 0.0f;

    private ulong currTrackingId = 0;
    public BirdMovement birdMovement;
    public bool GameStarted = false;
    float playerLength = 0.0f;


    // Use this for initialization
    void Start () {
        birdMovement = GameObject.Find("PlayerBird").GetComponent<BirdMovement>();
        if (BodySrcManager == null)
        {
            Debug.Log("BodySourceManager is null! Assign a bodysrcManager ");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();

        }
        flapthreshold = 0.18f;
    }

    void Update()
    {
        if (GameStarted == true)
        {
            body = GetActiveBody();
            if (body != null)
            {
                trackFlapping(body);
            }
        }
    }


    private void trackFlapping(Body body) //Basically measures the y-axis of both left and right hand of player. If the player moves the hands down at a particular pace and for a certain time, the bird will flap!
    {
        if (body.IsTracked)
        {
            currentLeftHandPositionY = body.Joints[HandLeft].Position.Y;
            currentRightHandPositionY = body.Joints[HandRight].Position.Y;

            timer += Time.deltaTime;

                if (timer > 0.10 && Time.time > nextFlap)
                {
                    if (previousLeftHandPositionY - currentLeftHandPositionY > flapthreshold && previousRightHandPositionY - currentRightHandPositionY > flapthreshold)
                    {
                        if (numberOfFlaps > 0) // To ignore the first flap, might be a better solution for this. 
                        {
                            birdMovement.doFlap();
                            nextFlap = Time.time + flapRate; // Used for cooldown, helps to prevent double triggering. 
                        }
                            numberOfFlaps++;
                    }
                    previousLeftHandPositionY = currentLeftHandPositionY;
                    previousRightHandPositionY = currentRightHandPositionY;
                    timer = 0f;
                }
        }
    }

    private Body GetActiveBody() // To locate the active player, in case there are many players in the background. 
    {
        if (bodyManager == null)
        {
            Debug.Log("Body manager is null!");
        }

        bodies = bodyManager.GetData();

        if (currTrackingId <= 0)
        {
            foreach (Body body in bodies)
            {
                if (body.IsTracked)
                {                  
                    currTrackingId = body.TrackingId;
                    return body;
                }
            }

            return null;
        }
        else
        {
            foreach (Body body in bodies)
            {
                if (body.IsTracked && body.TrackingId == currTrackingId)
                {
                    return body;
                }
            }
        }
        currTrackingId = 0;
        return GetActiveBody();
    }
}


