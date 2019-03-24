using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
	public GameObject pistol;
	public GameObject shotgun;
	public GameObject rpg;

	public Canvas deathScreen;

	public float reloading_timer = 0;
	public float reload_time;
	public bool shotgun_reload_in_progress = false;
	public Gun currentWeapon;
	public int ammo;
	public int capacity;
	public int loaded_ammo;

	public bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
		if ( Global.active_gun == 0 )
		{
			SetPistolActive();
		}
		else if ( Global.active_gun == 1 )
		{
			SetShotgunActive();
		}
		else if ( Global.active_gun == 2 )
		{
			SetRPGActive();
		}
    }

    // Update is called once per frame
    void Update()
    {
			if (!PauseMenuReal.IsPaused && alive ) {
				if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
				{
					SaveGunState();
					SetRPGActive();
				}
				if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
				{
					SaveGunState();
					SetShotgunActive();
				}
				if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
				{
					SaveGunState();
					SetPistolActive();
				}
				if( Input.GetMouseButtonDown(0) ) {
					shotgun_reload_in_progress = false;
				}
				if( reloading_timer <= 0 && Input.GetMouseButtonDown(0) && loaded_ammo > 0 )
				{
					currentWeapon.Fire();
					loaded_ammo--;
				}
				else if( reloading_timer <= 0 && Input.GetMouseButtonDown(0) )
				{
					currentWeapon.OutOfAmmo();
				} 
				else if( reloading_timer <= 0 && ( Input.GetKeyDown( KeyCode.R ) || shotgun_reload_in_progress ) && loaded_ammo != capacity && ammo != 0 )
				{
					if ( Global.active_gun == 1 ) {
						shotgun_reload_in_progress = true;
						loaded_ammo++;
						ammo--;
					} else {
						loaded_ammo = Math.Min(capacity,ammo);	
						ammo = ammo - Math.Min(capacity,ammo);
					}
					currentWeapon.Reload();
					reloading_timer = reload_time;
				}
				else if( reloading_timer > 0 ) 
				{
					reloading_timer = reloading_timer - ( 1 * Time.deltaTime );
				}
			}
    }


	void SetPistolActive()
	{
		Global.active_gun = 0;
		currentWeapon = pistol.GetComponent<pistol_shooting>();
		ammo = Global.pistol_ammo;
		capacity = Global.pistol_capacity;
		loaded_ammo = Global.loaded_pistol_ammo;
		reload_time = Global.pistol_reload_time;
		pistol.SetActive(true);
		shotgun.SetActive(false);
		rpg.SetActive(false);
		Debug.Log("set pistol active");
	}
	void SetShotgunActive()
	{
		Global.active_gun = 1;
		currentWeapon = shotgun.GetComponent<shotgun_shooting>();
		ammo = Global.shotgun_ammo;
		capacity = Global.shotgun_capacity;
		loaded_ammo = Global.loaded_shotgun_ammo;
		reload_time = Global.shotgun_reload_time;
		pistol.SetActive(false);
		shotgun.SetActive(true);
		rpg.SetActive(false);
		Debug.Log("set Shotgun active");
	}
	void SetRPGActive()
	{
		Global.active_gun = 2;
		currentWeapon = rpg.GetComponent<rpg_shooting>();
		ammo = Global.rpg_ammo;
		capacity = Global.rpg_capacity;
		loaded_ammo = Global.loaded_rpg_ammo;
		reload_time = Global.rpg_reload_time;
		pistol.SetActive(false);
		shotgun.SetActive(false);
		rpg.SetActive(true);
		Debug.Log("set RPG active");
	}

	void SaveGunState(){
		if ( Global.active_gun == 0 )
		{
			Global.pistol_ammo = ammo;
			Global.loaded_pistol_ammo = loaded_ammo;
		}
		else if ( Global.active_gun == 1 )
		{
			Global.shotgun_ammo = ammo;
			Global.loaded_shotgun_ammo = loaded_ammo;
		}
		else if ( Global.active_gun == 2 )
		{
			Global.rpg_ammo = ammo;
			Global.loaded_rpg_ammo = loaded_ammo;
		}
	}


    void OnTriggerEnter( Collider other )
	{
		if ( other.tag.Equals("Ghost") )
		{
			alive = false;
			Debug.Log("Crickey mate");
			deathScreen.gameObject.SetActive(true);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0f;
			GetComponent<FirstPersonController>().enabled = false;
		}
		Debug.Log("whopps hit " + other.tag);
	}
}