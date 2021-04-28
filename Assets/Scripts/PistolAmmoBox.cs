using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class PistolAmmoBox : MonoBehaviour {
    public float rotationSpeed = 120.0f;
    public int ammoPerBox = 8;
	public AudioSource clip;
     
    void Update () {
        transform.Rotate(Vector3.up,  rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
			clip.Play();
            Debug.Log("Pistol ammo get");
			if (Global.active_gun !=0 ){
            	Global.pistol_ammo += ammoPerBox;
			} else {
				other.GetComponent<newPlayerScript>().ammo += ammoPerBox;
			}
            Destroy(gameObject, clip.clip.length);
        }
    }
}