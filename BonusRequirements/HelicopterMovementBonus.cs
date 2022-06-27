using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;

public class HelicopterMovementBonus : MonoBehaviour
{
    LatLongAltitude lla = LatLongAltitude.FromDegrees(37.785668, -122.401270, 10.0);
    private double latitudeValue;
    private double longitudeValue;

    private Vector3 forwardMovement;
    private Vector3 rotationVector;
    private Rigidbody rigidBody;
    private Quaternion deltaRotation;
    public float turnForceMultiplier = 10.0f;

    [SerializeField] private float forwardThrust = 40;
    [SerializeField] private float backThrust = 30; 

    private void Start()
    {
        latitudeValue = lla.GetLatitude();
        longitudeValue = lla.GetLongitude();
        Api.Instance.SetOriginPoint(lla);
        rotationVector = new Vector3(0, turnForceMultiplier, 0);
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        forwardMovement = transform.forward * Input.GetAxis("Vertical");

        if (Input.GetAxis("Vertical")<0)
        {
            forwardMovement = forwardMovement.normalized * backThrust;
        }
        else if(Input.GetAxis("Vertical")>0)
        {
            forwardMovement = forwardMovement.normalized * forwardThrust;
        }
        
        lla = Api.Instance.SpacesApi.WorldToGeographicPoint(forwardMovement);
        Api.Instance.SetOriginPoint(lla);
    }

    private void FixedUpdate()
    {
        deltaRotation = Quaternion.Euler(rotationVector * Input.GetAxis("Horizontal") * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }
}
