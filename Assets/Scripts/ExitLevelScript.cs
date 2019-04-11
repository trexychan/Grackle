using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitLevelScript : MonoBehaviour
{
    public string sceneName;
    public Image img;
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;

    void Start()
    {
        img.enabled = true;
        img.CrossFadeAlpha(0.0f, fadeInTime, false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            img.CrossFadeAlpha(1.0f, fadeOutTime, false);
            StartCoroutine(WaitForFade());
			other.GetComponent<newPlayerScript>().SaveGunState();
        }
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(fadeOutTime + 0.1f);
        SceneManager.LoadScene(sceneName);
    }
}
