using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pistol_shooting : MonoBehaviour, Gun
{
	public GameObject 	bullet;
	public int 			bullet_speed;
	public float     	reload_time;
	public AudioClip 	gunshot;
	public AudioClip    reload;
	public AudioClip 	out_of_ammo;
	public AudioSource  gunshot_source;
	public AudioSource  reload_source;
	public AudioSource  out_of_ammo_source;

	void Start()
	{
		gunshot_source.clip = gunshot;
		reload_source.clip = reload;
	}
	/*
	void Update()
	{
		
		if ( reloading_timer <= 0 && Input.GetMouseButtonDown(0) && loaded_ammo > 0 )
		{
			Instantiate( bullet, transform.position, transform.rotation );
			loaded_ammo--;
			gunshot_source.Play();
		}

		else if ( reloading_timer <= 0 && Input.GetMouseButtonDown(0) )
		{
			out_of_ammo_source.Play();
		}

		else if ( reloading_timer <= 0 && Input.GetKeyDown( KeyCode.R ) )
		{
			loaded_ammo = max_ammo;	
			reload_source.Play();
			reloading_timer = reload_time;
		}
		else if ( reloading_timer > 0 ) 
		{
			reloading_timer = reloading_timer - ( 1 * Time.deltaTime );
		}
	}
*/	
	public void Fire()
	{
		Instantiate( bullet, transform.position, transform.rotation );
		gunshot_source.Play();
	}

	public void OutOfAmmo()
	{
		out_of_ammo_source.Play();
	}

	public void Reload()
	{
		reload_source.Play();
	}
}
