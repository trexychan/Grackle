using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public GameObject pistol;
	public GameObject shotgun;
	public GameObject rpg;

	public Canvas deathScreen;

	private float reloading_timer = 0;
	private Gun currentWeapon;
	private int ammo;
	private int capacity;
	private int loaded_ammo;
	private float reload_time;

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
    }

    // Update is called once per frame
    void Update()
    {
		
		if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
		{
			SetShotgunActive();

		}
		if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
		{
			SetPistolActive();
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

		else if( reloading_timer <= 0 && Input.GetKeyDown( KeyCode.R ) )
		{
			loaded_ammo = capacity;	
			currentWeapon.Reload();
		}
		else if( reloading_timer > 0 ) 
		{
			reloading_timer = reloading_timer - ( 1 * Time.deltaTime );
		}
    }


	void SetPistolActive()
	{
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



    void OnTriggerEnter( Collider other )
	{
		if ( other.tag.Equals("Ghost") )
		{

			Debug.Log("Crickey mate");
			deathScreen.gameObject.SetActive(true);
			Time.timeScale = 0f;
		}
		Debug.Log("whopps");
	}
}