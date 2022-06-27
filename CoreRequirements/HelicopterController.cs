using UnityEngine;
using System;
using Wrld.Space;
using Wrld;

public class HelicopterController : MonoBehaviour
{
//  (1) MOVEMENT USING CORE REQUIREMENT

    // Constant forward thrust of helicopter.
    public float forwardThrustForce = 4.0f;
    public float backThrustForce = 3.0f;
    // Turning force as a multiple of the thrust force.
    public float turnForceMultiplier = 10.0f;

    private Vector3 forwardMovement;
    private Vector3 rotationVector;
    private Rigidbody rigidBody;
    private Quaternion deltaRotation;

    // Use this for initialization
    void Start()
    {
        // Find the RigidBody component and save a reference to it
        rigidBody = GetComponent<Rigidbody>();
        rotationVector = new Vector3(0, turnForceMultiplier, 0);
    }

    // Update is called once per frame
    void Update()
    {
        forwardMovement = transform.forward * Input.GetAxis("Vertical");

        ////If up key is pressed forward thrust and if down key is pressed backward thrust
        if (Input.GetAxis("Vertical") < 0)
        {
            forwardMovement = forwardMovement.normalized * backThrustForce;
        }
        else
        {
            forwardMovement = forwardMovement.normalized * forwardThrustForce;
        }
    }

    void FixedUpdate()
    {
        //For forward and backward movement of helicopter using rigidbody component
        rigidBody.MovePosition(rigidBody.position + forwardMovement);

        //The rotation logic using moverotation
        deltaRotation = Quaternion.Euler(rotationVector * Input.GetAxis("Horizontal") * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }
}
