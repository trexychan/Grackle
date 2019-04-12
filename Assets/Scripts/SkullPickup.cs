using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkullPickup : MonoBehaviour
{
    public static int skullCount;
    public static int skullInitial;
	public Text counter;

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
			Debug.Log("Skulls remaining" +skullCount);
			counter.text = "SKULLS REMAINING: "+skullCount;
			if (skullCount == 0){
                EventManager.SkullsCollected();
				Debug.Log("Skulls collected");
			}
            Destroy(gameObject, audioSrc.clip.length);
        }
    }

    public static void ResetSkullCount()
    {
        skullCount = 0;
        skullInitial = 0;
    }
}
