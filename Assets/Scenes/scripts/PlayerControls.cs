using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float thrustSpeed;
    public float turnSpeed;
    public float hoverPower;
    public float hoverHeight;

    private float thrustInput;
    private float turnInput;
    private Rigidbody PlayerShip;

    // Use this for initialization
    void Start () {
        PlayerShip = GetComponent<Rigidbody>();
    }
        
        // Update is called once per frame
        void Update () {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // accelarate the ship 
        if (Input.GetKey(KeyCode.Space)) {
            PlayerShip.AddRelativeForce(Vector3.forward * 7, ForceMode.Impulse);
        }
    }

    void FixedUpdate() {
        // Turning the ship
        PlayerShip.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

        // Moving the ship
        PlayerShip.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);

        // Hovering
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverPower;
            PlayerShip.AddForce(appliedHoverForce, ForceMode.Acceleration);
        if (hit.distance < hoverHeight / 2) {
            appliedHoverForce *= 2;
            }
        }
      
        // lower the ship down to ground using LEFT shift
        if (Input.GetKey(KeyCode.LeftShift)) {
            hoverHeight = 0;
        }else{
            hoverHeight = 2;
        }
    }
    
}