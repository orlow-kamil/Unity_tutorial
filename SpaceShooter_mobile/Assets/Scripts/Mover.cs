using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	private Rigidbody rb;

	public float speed;

	void Start () 
	{
		rb = GetComponent <Rigidbody> ();
		//prędkość jako wektor ruchu naprzód wzdłuż osi z (zwana NIEBIESKA)
		rb.velocity = transform.forward * speed;
	}

}
