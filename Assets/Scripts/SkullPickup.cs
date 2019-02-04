using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPickup : MonoBehaviour
{
    private static int skullCount;
    private static int skullInitial;

    private AudioSource audioSrc;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        skullCount++;
        skullInitial++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            audioSrc.pitch = (1 - ((float)skullCount / skullInitial)) * 2 + 1;
            audioSrc.Play();
            meshRenderer.enabled = false;

            skullCount--;

            if (skullCount == 0)
                EventManager.SkullsCollected();

            Destroy(gameObject, audioSrc.clip.length);
        }
    }
}
