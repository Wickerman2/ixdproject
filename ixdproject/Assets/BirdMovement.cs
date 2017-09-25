using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

    public Vector3 flapVelocity; 
    bool didFlap = false;
    public float maxSpeed = 5f;
    float forwardSpeed = 1f;
    public Rigidbody2D rb;
    float flapSpeed = 200f;
    Animator animator; 


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.Log("ani");
        }
	}
    // For Graphic & Input updates
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            didFlap = true;
        }
    }

    // For physic engine updates
    void FixedUpdate () {
        rb.AddForce(Vector2.right * forwardSpeed);

        if (didFlap)
        {
            animator.SetTrigger("DoFlap");
            rb.AddForce(Vector2.up * flapSpeed);

            didFlap = false;
        }
        /*
        if (rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            float angle = Mathf.Lerp(0, -90, rb.velocity.y / 2f);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            */
        }



    /*velocity.x = forwardSpeed; 

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



    // To make the bird angle downwards
    float angle = 0;
    if (velocity.y < 0)
    {
        angle = Mathf.Lerp(0, -90, -velocity.y / maxSpeed);
    }

    transform.rotation = Quaternion.Euler(0, 0, angle);*/
}
