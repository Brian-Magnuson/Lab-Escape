using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    //This class should be placed on a camera
    [Header("Camera Bounds")]
    [Tooltip("These are the boundaries of where the camera can move in worldspace")]
    public float MaxXBoundary = 5f, MinXBoundary = -5f, MaxYBoundary = 5f, MinYBoundary = -5f;
    [Tooltip("Check this if you want the camera to have boundaries")]
    public bool clampCamera = true;

    [Space]
    [Header("Object to Follow")]
    [Tooltip("This is the character in which the camera will be following")]
    public Transform PlayerCharacter;

    [Space]
    [Header("Speed")]
    [Tooltip("This is the speed at which the camera follows the player, the smaller the value, the faster the movement")]
    public float smoothSpeed = 0.125f; //The bigger the value, the faster the smooth


    private Vector3 Velocity = Vector3.zero;//This is just something we use as a reference for the smoothdamp
    private Camera camera;//This is the camera we using, it's gonna be handled in start

    void Start()
    {
        if (PlayerCharacter == null)//We make sure that the player is found somewhere
        {
            PlayerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        }

        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 point = camera.WorldToViewportPoint(PlayerCharacter.position);//Grab where the player is
        Vector3 delta = PlayerCharacter.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //We have the vector between the two
        Vector3 destination = transform.position + delta;//And we have where we need to go
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref Velocity, smoothSpeed);//So now we make that look smooth

        Transform camerATransform = camera.gameObject.transform;//We now are gonna check to see if the camera is within it's bounds

        //Clamp the player between some bounds
        if (clampCamera)
        {
            if (camerATransform.position.x > MaxXBoundary || camerATransform.position.x < MinXBoundary || camerATransform.position.y > MaxYBoundary || camerATransform.position.y < MinYBoundary)
            {
                float x = Mathf.Clamp(PlayerCharacter.position.x, MinXBoundary, MaxXBoundary);
                float y = Mathf.Clamp(PlayerCharacter.position.y, MinYBoundary, MaxYBoundary);
                transform.position = new Vector3(x, y, gameObject.transform.position.z);
            }
        }
    }
}
