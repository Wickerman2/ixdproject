using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class BirdMovement : MonoBehaviour
{

    public float flapSpeed = 150f;
    public float forwardSpeed = 1f;
    SerialPort myData = new SerialPort("COM4", 19200);

    bool didFlap = false;

    Animator animator;

    public bool dead = false;
    float deathCooldown;

    public bool godMode = false;

    // Use this for initialization
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("Didn't find animator!");
        }
    }

    // Do Graphic & Input updates here
    void Update()
    {

        if (dead)
        {
            deathCooldown -= Time.deltaTime;

            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    //Application.LoadLevel( Application.loadedLevel );
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                //didFlap = true;
                doFlap();
            }
        }
    }


    // Do physics engine updates here
    void FixedUpdate()
    {

        if (dead)
            return;

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * forwardSpeed);

        if (didFlap)
        {
          //  DoFlap();
        }
        /*
		if(GetComponent<Rigidbody2D>().velocity.y > 0) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else {
			float angle = Mathf.Lerp (0, -15, (-GetComponent<Rigidbody2D>().velocity.y / 3f) );
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
        */
    }

    public void doFlap ()
    {
        animator.SetTrigger("DoFlap");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * flapSpeed);

        //didFlap = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(godMode)
			return;

		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
        */
        /*
        if (collision.gameObject.tag == "Seed")
        {
            Score.AddPoint();
        }
        */
    }
}
