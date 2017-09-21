using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
    public Vector3 gravity;
    public Vector3 flapVelocity; 
    bool didFlap = false;
    public float maxSpeed = 5f;
    public float forwardSpeed = 1f;


    // Use this for initialization
    void Start () {
		
	}

    // For Graphic & Input updates
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            didFlap = true;
        }
    }

    // For physic engine updates
    void FixedUpdate () {
        velocity += gravity * Time.deltaTime;
        velocity.x = forwardSpeed; 

        if (didFlap == true)
        {
            didFlap = false;
            if (velocity.y < 0)
            {
                velocity.y = 0;
            }

            velocity += flapVelocity;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;

        // To make the bird angle downwards
        float angle = 0;
        if (velocity.y < 0)
        {
            angle = Mathf.Lerp(0, -90, -velocity.y / maxSpeed);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
