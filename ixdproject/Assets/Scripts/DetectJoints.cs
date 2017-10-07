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
    float timer2;
    public float flapthreshold;
    bool flapInProgress = false;


    GameObject LeftHandCube;
    GameObject RightHandCube;
    Transform leftHandTransform;
    Transform rightHandTransform;
    int bodyCount;
    private Body[] bodies;
    public Body body;

    float previousLeftHandPositionY;
    float previousRightHandPositionY;
    float currentLeftHandPositionY;
    float currentRightHandPositionY;
    private ulong currTrackingId = 0;
    public BirdMovement instanceOfBM;
    int flapCount = 0;

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
    }

    void Update()
    {
        body = GetActiveBody();
        if (body != null)
        {
            trackBody(body);
        }
    }

    private void trackBody(Body body)
    {
        if (body.IsTracked)
        {
            currentLeftHandPositionY = body.Joints[HandLeft].Position.Y;
            currentRightHandPositionY = body.Joints[HandRight].Position.Y;

            timer += Time.deltaTime;
            if (timer > 0.20f)
            {
                if (previousLeftHandPositionY - currentLeftHandPositionY > flapthreshold && previousRightHandPositionY - currentRightHandPositionY > flapthreshold)
                {
                        instanceOfBM.doFlap();
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


