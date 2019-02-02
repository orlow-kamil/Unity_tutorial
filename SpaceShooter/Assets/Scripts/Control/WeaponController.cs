 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	private AudioSource aus;

	void Start () 
	{
		aus = GetComponent<AudioSource> ();
		//powtórz metode trwajaca dany czas co pewien okres czasu 
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		aus.Play ();
	}

}

