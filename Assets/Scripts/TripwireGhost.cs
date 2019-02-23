using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TripwireGhost : MonoBehaviour
{
    public Rigidbody cardboard;


    void OnCollisionEnter(Collision other) {

        Debug.Log("CC");
        DoGag();
    }
    public void DoGag() {

        cardboard.useGravity = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("TE");
            DoGag();
        }
    }
}
