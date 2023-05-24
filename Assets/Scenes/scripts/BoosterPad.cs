using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPad : MonoBehaviour {
    public GameObject player;
    private float turboDur;
    [SerializeField]
    private float turboAmount;
    [SerializeField]
    private float turboDuration;
    private float lastTurboAt;
    private  bool isBoosting = false;

    void Start() {
        lastTurboAt = -turboDuration;
    }

    void FixedUpdate() {
        if (Time.time - lastTurboAt < turboDuration) {
            player.GetComponent<Rigidbody>().AddForce(transform.forward * turboAmount);
        }
    }

    private void OnTriggerEnter(Collider other) {
        lastTurboAt = Time.time;
    if (Time.time - lastTurboAt >= turboDur) {
        if (other.tag == "Player") {
            isBoosting = true;
            lastTurboAt = Time.time;
        }
    }
}
}