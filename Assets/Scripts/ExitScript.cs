using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private AudioSource audioSrc;
    private bool isOpen;
    private Quaternion rotationGoal;

    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        isOpen = false;
        EventManager.OnSkullsCollected += Exit;
    }

    void Update()
    {
        // Spherically interpolate 90 degrees
        if (isOpen)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                rotationGoal, Time.deltaTime * rotationSpeed);
        }

        // Once door is opened, stop this entire script from running
        if (Quaternion.Angle(rotationGoal, transform.localRotation) < 2f)
        {
            this.enabled = false;
        }
    }

    void Exit()
    {
        audioSrc.Play();
        rotationGoal = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y - 90f, 0);
        isOpen = true;
    }
}
