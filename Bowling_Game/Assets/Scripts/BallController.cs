using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour
{
    public float speed;
    public Vector3 startPos;
    public Quaternion startRot;
    private Rigidbody rb;
    public int ballThrow = 0;
    bool doUpdate = false;
    public AudioClip pinSound;
    float volume = 1f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;
    }

    // Ball movement
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    // Checks for collision with pin, to play sound effect
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            AudioSource.PlayClipAtPoint(pinSound, transform.position, volume);
        }
    }

    // Resets the ball to its original position and sets all other the values to zero
    public void Reset(int ballThrow)
    { 
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // From here on the code focusses on resetting everything after a ball throw

    void LateUpdate()
    {
    // Checks if the Update has to be done and start the coroutine to delay the update
        if (doUpdate)
        {
            ballThrow += 1;
            ballThrow = ballThrow % 3;
            StartCoroutine(DelayUpdate());
        }
        doUpdate = false;
    }

    // Checks if the ball collides with the Ball Reset Trigger and resets the ball
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            doUpdate = true;
            SendMessage("Reset", ballThrow, SendMessageOptions.DontRequireReceiver);
        }
    }

    // Resets the pins after each frame (so after 2 ballThrows)
    void ResetFrame()
    {
        Debug.Log("Resetting pins");

        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            pin.SendMessage("ResetPin", ballThrow, SendMessageOptions.DontRequireReceiver);
        }
        ballThrow = 0;
    }

    // Delays the update so the system had time to calculate the score and update this.
    public IEnumerator DelayUpdate()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1f);
        Debug.Log("scoring");
        SendMessage("UpdateScore", ballThrow, SendMessageOptions.RequireReceiver);
        yield return 0;
    }
}