using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_script : MonoBehaviour
{
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        skull_pickup.OnPickup += Exit;
    }

    void Exit()
    {
        audioSrc.Play();
    }
}
