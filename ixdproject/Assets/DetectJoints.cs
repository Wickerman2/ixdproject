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
    public float multiplier = 10f;
    float timer;
    public float threshold;

    GameObject LeftHandCube;
    GameObject RightHandCube;
    Transform leftHandTransform;
    Transform rightHandTransform;
    int bodyCount;
    private Body[] bodies;

    float previousLeftHandPositionY;
    float previousRightHandPositionY;
    float currentLeftHandPositionY;
    float currentRightHandPositionY;

    public BirdMovement instanceOfBM;

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
	
	// Update is called once per frame
	void Update () {



        if (bodyManager == null)
        {
            Debug.Log("Body manager is null!");
            return;
        }
        bodies = bodyManager.GetData();

        //Debug.Log(Time.time);

        if (bodies == null)
        {
            return;
        }
        foreach (var body in bodies)
        {
            //bodyCount++;
            //Debug.Log("Bodycount: " + bodyCount);
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                currentLeftHandPositionY = body.Joints[HandLeft].Position.Y;
                currentRightHandPositionY = body.Joints[HandRight].Position.Y;

                timer += Time.deltaTime;
                if (timer > 0.2f)
                {
                    if (previousLeftHandPositionY - currentLeftHandPositionY > threshold && previousRightHandPositionY - currentRightHandPositionY > threshold)
                    {
                        Debug.Log("Flap!");
                        instanceOfBM.doFlap();

                        //instanceOfB.GetComponent<BirdMovement>().doFlap();
                        //Debug.Log("Diff Left " + (currentLeftHandPositionY - previousLeftHandPositionY));
                        //Debug.Log("Diff Right " + (currentRightHandPositionY - previousRightHandPositionY));
                    }
                    //Debug.Log(previousLeftHandPositionY);
                    //Debug.Log(previousRightHandPositionY);
                    previousLeftHandPositionY = currentLeftHandPositionY;
                    previousRightHandPositionY = currentRightHandPositionY;
                    timer = 0f;
                }

                /*
                var LeftHandPosition = body.Joints[HandLeft].Position;
                var RightHandPosition = body.Joints[HandRight].Position;



                //Debug.Log("Current Left : " + currentLeftHandPosition.Y);
                //Debug.Log("Current Right : " + currentRightHandPosition.Y);



                LeftHandCube = GameObject.Find("LeftHandCube");
                RightHandCube = GameObject.Find("RightHandCube");

                LeftHandCube.transform.position = new Vector3(LeftHandPosition.X * multiplier, LeftHandPosition.Y * multiplier);
                RightHandCube.transform.position = new Vector3(RightHandPosition.X * multiplier, RightHandPosition.Y * multiplier);
                
                if (currentLeftHandPosition.Y > 0.0 && currentRightHandPosition.Y > 0.0f)
                {
                    //Debug.Log("Uppe!");
                }
                else if(currentLeftHandPosition.Y < 0.0  && currentRightHandPosition.Y < 0.0f)
                {
                    //Debug.Log("Ner!");
                }*/

            }
            else if(!body.IsTracked)
            {
                //Debug.Log("No body tracked!");
            }
        }
	}
}
