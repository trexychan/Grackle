using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private AudioSource audioSrc;
    private bool isOpen;
    private Quaternion rotationGoal;
    private float swingDirection;

    public float rotationSpeed = 10f;
    public bool reverseSwingDirection;

    public static ExitScript instance;

    // Start is called before the first frame update
    void OnLevelWasLoaded()
    {
        audioSrc = GetComponent<AudioSource>();
        isOpen = false;
        swingDirection = reverseSwingDirection ? 90f : -90f;
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
        if (audioSrc)
            audioSrc.Play();

        EventManager.OnSkullsCollected -= Exit;

        rotationGoal = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y + swingDirection, 0);
        isOpen = true;
    }
}
