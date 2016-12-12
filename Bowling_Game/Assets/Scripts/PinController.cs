using UnityEngine;
using System.Collections;

public class PinController : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(true);
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    // Resets the pins to their original position and sets all other the values to zero.
    public void ResetPin(int ballThrow)
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
