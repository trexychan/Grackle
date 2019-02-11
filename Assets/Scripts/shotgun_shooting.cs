using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun_shooting : MonoBehaviour, Gun
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
