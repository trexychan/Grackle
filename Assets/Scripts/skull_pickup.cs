using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull_pickup : MonoBehaviour
{
    private static int skullCount;
    private static int skullInitial;
    private AudioSource audioSrc;

    public delegate void PickupAction();
    public static event PickupAction OnPickup;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        skullCount++;
        skullInitial++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            audioSrc.pitch = (1 - ((float)skullCount / skullInitial)) * 2 + 1;
            audioSrc.Play();
            skullCount--;
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            if (OnPickup != null && skullCount == 0)
                OnPickup();

            Destroy(gameObject, audioSrc.clip.length);
        }
    }
}
