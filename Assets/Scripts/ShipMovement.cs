using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

    public float sailSpeed = 1;
    public float rowSpeed = 1;
    public float turnSpeed = 1f; //degrees per frame update
    public float rotSpeed; //Velocity modified turn speed

    //Zones: Run, Beam, Broad, Close
    public float[] mods = new float[4] { 0.75f, 1f, 0.5f, 0.25f };

    private Rigidbody rb;
    private bool sailsUp;

	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
        sailsUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            sailsUp = !sailsUp;
        }
        if (!sailsUp)
        {
            if (Input.GetKey("w")) {
                rb.AddForce(transform.forward * rowSpeed);
            }
            rotSpeed = turnSpeed;
        }

        //Turning
        if (Input.GetKey("d")) {
            Quaternion q = Quaternion.AngleAxis(rotSpeed, transform.up) * transform.rotation;
            rb.MoveRotation(q);
            float mag = rb.velocity.magnitude;
            rb.velocity = transform.forward * mag * 0.995f;
        }
        if (Input.GetKey("a")) {
            Quaternion q = Quaternion.AngleAxis(-rotSpeed, transform.up) * transform.rotation;
            rb.MoveRotation(q);
            float mag = rb.velocity.magnitude;
            rb.velocity = transform.forward * mag * 0.995f;
        } 
    }

    public void WindMovement(float power, float range)
    {
        int zone = 3;
        if(range < 30) {
            zone = 0;
        } else if (range < 75) {
            zone = 1;
        } else if (range < 105) {
            zone = 2;
        }
        //Speed of turning is based on current velocity of the ship
        rotSpeed = (Mathf.Abs(rb.velocity.magnitude - 10f) / 10 + 1) * turnSpeed;
 
        float force = power * sailSpeed * mods[zone];
        rb.AddForce(transform.forward * force);
    }

    public bool GetSailStatus()
    {
        return sailsUp;
    }
}
