using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{ 
    public GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Transforms the camera to the player position + offset.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}