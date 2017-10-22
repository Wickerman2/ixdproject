using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DetectJoints : MonoBehaviour
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
    public BirdMovement instanceOfBM;
    public bool GameStarted = false;
    float playerLength = 0.0f;


    // Use this for initialization
    void Start () {
        instanceOfBM = GameObject.Find("PlayerBird").GetComponent<BirdMovement>();
        if (BodySrcManager == null)
        {
            Debug.Log("BodySourceManager is null! Assign a bodysrcManager ");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();

        }

        playerLength = PlayerPrefs.GetFloat("PlayerLength");
        //flapthreshold = (playerLength / 6.0f);
        flapthreshold = 0.18f;
    }

    void Update()
    {
        if (GameStarted == true)
        {
            body = GetActiveBody();
            if (body != null)
            {
                trackBody(body);
            }
        }
    }


    private void trackBody(Body body)
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
                        if (numberOfFlaps > 0)
                        {
                            instanceOfBM.doFlap();
                            nextFlap = Time.time + flapRate;
                        }
                            numberOfFlaps++;
                    }
                    previousLeftHandPositionY = currentLeftHandPositionY;
                    previousRightHandPositionY = currentRightHandPositionY;
                    timer = 0f;
                }
        }
    }

    private Body GetActiveBody()
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


