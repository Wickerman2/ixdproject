using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class BirdMovement : MonoBehaviour
{

    public float flapSpeed = 150f;
    public float forwardSpeed = 8f;
    SerialPort myData = new SerialPort("COM4", 19200);

    public AudioClip flapAudio;
    public AudioSource audioSource;


    Animator animator;

    public bool dead = false;
    float deathCooldown;
    float distance;
    public bool godMode = false;

    // Use this for initialization
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        audioSource.clip = flapAudio;

        if (animator == null)
        {
            Debug.LogError("Didn't find animator!");
        }

    }

    // Do Graphic & Input updates here
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doFlap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Berry"))
        {
            animator.SetTrigger("DoOpenMouth");
        }
    }

    // Do physics engine updates here
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * forwardSpeed);
    }

    public void doFlap ()
    {
        audioSource.PlayOneShot(flapAudio);
        animator.SetTrigger("DoFlap");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * flapSpeed);
    }

}
