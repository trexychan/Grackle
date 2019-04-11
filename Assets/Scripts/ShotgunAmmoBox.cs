using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmoBox : MonoBehaviour
{
    public float rotationSpeed = 120.0f;
    public int ammoPerBox = 4;
     
    void Update () {
        transform.Rotate(Vector3.up,  rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            Debug.Log("Shotgun ammo get");
			if (Global.active_gun != 1 ){
	            Global.shotgun_ammo += ammoPerBox;
			} else {
				other.GetComponent<newPlayerScript>().ammo += ammoPerBox;
			}
            Destroy(gameObject);
        }
    }
}
