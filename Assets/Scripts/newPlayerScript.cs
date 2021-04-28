using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class newPlayerScript : MonoBehaviour
{
	public GameObject pistol;
	public GameObject shotgun;
	public GameObject rpg;

	public Canvas deathScreen;
	public Canvas pauseScreen;
	public Canvas UIScreen;
	public Image  AmmoCanvas;
	public GameObject PistolAmmoDisplay;
	public GameObject ShotgunAmmoDisplay;


	public float reloading_timer = 0f;
	public float reload_time;
	public float fire_delay = .5f;
	public float fire_delay_timer = 0f;
	public bool shotgun_reload_in_progress = false;
	public Gun currentWeapon;
	private int last_frame_ammo;
	private int last_frame_loaded_ammo;
	public int ammo;
	public int capacity;
	public int loaded_ammo;

	public bool alive = true;

	public Animator anim;
	public Transform mesh;
	public Vector3 pos;

	//Variables for difficulty selection
	public GameObject trail;
	public GameObject minimap_camera;
	public Image minimap;


    // Start is called before the first frame update
    void Start()
    {
		ReloadLastGunState();
		mesh = transform.GetChild(0).GetChild(0);
		anim = mesh.gameObject.GetComponent<Animator>();
		deathScreen.gameObject.SetActive(false);
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
		if ( Global.crosshair ) {
			pauseScreen.GetComponent<PauseMenuReal>().crosshair.SetActive(true);
		} else {
			pauseScreen.GetComponent<PauseMenuReal>().crosshair.SetActive(false);
		}
		UIScreen.GetComponentInChildren<Text>().text = "Skull Remaining: " + SkullPickup.skullCount;
		last_frame_ammo = ammo;
		last_frame_loaded_ammo = loaded_ammo;
		if ( ammo == 0 ) {
			AmmoCanvas.GetComponentInChildren<Text>().enabled = true;
			if ( loaded_ammo == 0 ) {
				ShotgunAmmoDisplay.SetActive(false);
				PistolAmmoDisplay.SetActive(false);
				AmmoCanvas.GetComponentInChildren<Text>().text = "Out Of Ammo";
			} else {
				AmmoCanvas.GetComponentInChildren<Text>().text = "No Spare Ammo";
			}
		} else {
			AmmoCanvas.GetComponentInChildren<Text>().enabled = false;
		}
		Account_for_difficulty();
    }

    // Update is called once per frame
    void Update()
    {
		pos = mesh.localPosition;
		if (!PauseMenuReal.IsPaused && alive ) {
			/*
			if (  Global.active_gun != 2 && Input.GetKeyDown( KeyCode.Alpha3 ) )
			{
				anim.SetInteger("Gun", 2);
				SaveGunState();
				SetRPGActive();
			}
			*/
			if ( Global.active_gun != 1 && CrossPlatformInputManager.GetButtonDown("Weapon2") )
			{
				mesh.localPosition = new Vector3(mesh.localPosition.x, mesh.localPosition.y - .23f, mesh.localPosition.z);
				anim.SetInteger("Gun", 1);
				SaveGunState();
				SetShotgunActive();
			}
			if ( Global.active_gun != 0 && CrossPlatformInputManager.GetButtonDown("Weapon1") )
			{
				mesh.localPosition = new Vector3(mesh.localPosition.x, mesh.localPosition.y + .23f, mesh.localPosition.z);
				anim.SetInteger("Gun", 0);
				SaveGunState();
				SetPistolActive();
			}
			if( CrossPlatformInputManager.GetButtonDown("Fire1") ) {
				shotgun_reload_in_progress = false;
			}
			if (fire_delay_timer < 0 ){
				anim.ResetTrigger("Shoot");
				if( reloading_timer <= 0 && CrossPlatformInputManager.GetButtonDown("Fire1") && loaded_ammo > 0 )
				{
					fire_delay_timer = fire_delay;
					anim.SetTrigger("Shoot");
					currentWeapon.Fire();
					loaded_ammo--;
				}
				else if( reloading_timer <= 0 && CrossPlatformInputManager.GetButtonDown("Fire1") )
				{
					currentWeapon.OutOfAmmo();
				} 
				else if( reloading_timer <= 0 && ( CrossPlatformInputManager.GetButtonDown("Reload") || shotgun_reload_in_progress || loaded_ammo == 0 ) && loaded_ammo != capacity && ammo != 0 )
				{
					anim.SetTrigger("Reload");
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
					anim.ResetTrigger("Reload");
					reloading_timer -= Time.deltaTime ;
				}
			} else {
				fire_delay_timer -= Time.deltaTime;
			}

			//Handle Ammo Remaining Icons
			if ( ammo != last_frame_ammo || loaded_ammo != last_frame_loaded_ammo ) {
				if ( ammo == 0 ) {
					ShotgunAmmoDisplay.SetActive(false);
					PistolAmmoDisplay.SetActive(false);
					AmmoCanvas.GetComponentInChildren<Text>().enabled = true;
					if ( loaded_ammo == 0 ) {
						AmmoCanvas.GetComponentInChildren<Text>().text = "Out Of Ammo";
					} else {
						AmmoCanvas.GetComponentInChildren<Text>().text = "No Spare Ammo";
					}
				} else {
					AmmoCanvas.GetComponentInChildren<Text>().enabled = false;
					if ( Global.game_mode != 2 ) {
						DisplayCurrentAmmo();
					}
				}
			}
			last_frame_ammo = ammo;
			last_frame_loaded_ammo = loaded_ammo;
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
		//rpg.SetActive(false);
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
		//rpg.SetActive(false);
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

	public void SaveGunState(){
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

	public void ReloadLastGunState(){
		Global.pistol_ammo = Global.last_pistol_ammo;
		Global.shotgun_ammo = Global.last_shotgun_ammo;
		Global.rpg_ammo = Global.last_rpg_ammo;
		Global.loaded_pistol_ammo = Global.last_loaded_pistol_ammo;
		Global.loaded_shotgun_ammo = Global.last_loaded_shotgun_ammo;
		Global.loaded_rpg_ammo = Global.last_loaded_rpg_ammo;
	}

	public void SaveFinalGunState(){
		SaveGunState();
		Global.last_pistol_ammo = Global.pistol_ammo;
		Global.last_shotgun_ammo = Global.shotgun_ammo;
		Global.last_rpg_ammo = Global.rpg_ammo;
		Global.last_loaded_pistol_ammo = Global.loaded_pistol_ammo;
		Global.last_loaded_shotgun_ammo = Global.loaded_shotgun_ammo;
		Global.last_loaded_rpg_ammo = Global.loaded_rpg_ammo;
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
		} else if ( other.tag.Equals("Skull") ) {
			UIScreen.GetComponentInChildren<Text>().text = "Skull Remaining: " + SkullPickup.skullCount;
		}
		Debug.Log("whopps hit " + other.tag);
	}

	void DisplayCurrentAmmo(){
		if ( Global.active_gun == 0 ) {
			ShotgunAmmoDisplay.SetActive(false);
			PistolAmmoDisplay.SetActive(true);
			if ( last_frame_ammo/capacity <= 6 ) {
				for ( int i = 0; i < 6 ; i++ ) {
					PistolAmmoDisplay.transform.GetChild(i).gameObject.SetActive(false);
				}
				for ( int i = 0; i < Math.Min( 6, ammo/capacity ) ; i++ ) {
					PistolAmmoDisplay.transform.GetChild(i).gameObject.SetActive(true);
				}
			}
		} else {
			ShotgunAmmoDisplay.SetActive(true);
			PistolAmmoDisplay.SetActive(false);
			if ( last_frame_ammo <= 9 ) {
				for ( int i = 0; i < 10 ; i++ ) {
					ShotgunAmmoDisplay.transform.GetChild(i).gameObject.SetActive(false);
				}
				for ( int i = 0; i < Math.Min( 10, ammo ) ; i++ ) {
					ShotgunAmmoDisplay.transform.GetChild(i).gameObject.SetActive(true);
				}
			}
			if ( ammo > 9 ) {
				ShotgunAmmoDisplay.GetComponentInChildren<Text>().text = "+" + (ammo - 9);
			}
		}
	}

	void Account_for_difficulty()
	{
		switch ( Global.game_mode ) // 0 = normal, 1 = difficult,  2 = more difficult
		{
			case 0:
				Debug.Log("Normal Mode");
				minimap_camera.gameObject.SetActive(true);
				minimap.gameObject.SetActive(true);
				trail.gameObject.SetActive(true);
				UIScreen.gameObject.SetActive(true);
				DisplayCurrentAmmo();
				break;	
			case 1:
				Debug.Log("Difficult Mode");
				minimap_camera.gameObject.SetActive(false);
				minimap.gameObject.SetActive(false);
				trail.gameObject.SetActive(false);
				UIScreen.gameObject.SetActive(true);
				DisplayCurrentAmmo();
				break;
			case 2:	
				Debug.Log("More Difficult Mode");
				minimap_camera.gameObject.SetActive(false);
				minimap.gameObject.SetActive(false);
				trail.gameObject.SetActive(false);
				UIScreen.gameObject.SetActive(false);
				break;
			default:
				Debug.Log("Defaulting to Normal Mode");
				minimap_camera.gameObject.SetActive(true);
				minimap.gameObject.SetActive(true);
				trail.gameObject.SetActive(true);
				UIScreen.gameObject.SetActive(true);
			    DisplayCurrentAmmo();
				break;
			
		}
	}
}
