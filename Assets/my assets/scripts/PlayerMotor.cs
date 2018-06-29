using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    //to actually move the player object, we need to reference to player object's rigidbody component. this will allow us to move the player

    public Camera cam;


    private Rigidbody rb;
    
    

    private Vector3 velocity = Vector3.zero; //we will store the _velocity variable from PlayerController in this script under the name "velocity"
    private Vector3 rotation = Vector3.zero; //we will store the _rotation variable from PlayerController in this script under the name "rotation"
    private Vector3 camRotation = Vector3.zero;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	

    public void ReceiveVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void ReceiveRotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void ReceiveCamRotation(Vector3 _camRotation)
    {
        camRotation = _camRotation;
    }

    //below are the functions which actually move or rotate the player object's rigidbody

    void FixedUpdate() //we call the perform functions inside of FixedUpdate because it renders on the physics frame, not the timed frame
    {
        PerformMovement();
        PerformRotation();
    }

    public void PerformMovement()
    {
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime); //take our current pos and add it to our velocity
    }

    public void PerformRotation()
    {
        if (cam != null)
        {
            cam.transform.rotation = cam.transform.rotation * Quaternion.Euler(camRotation); //take our camera's current rotation and multiply it by the camrotation
        }

        rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation)); //multiply our current rotation by the rotation calculation we did inside of PerformRotation
    }

}
