using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        EventManager.OnSkullsCollected += Exit;
    }

    void Exit()
    {
        audioSrc.Play();
    }
}
