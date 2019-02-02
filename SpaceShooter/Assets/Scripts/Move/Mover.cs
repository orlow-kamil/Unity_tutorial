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

		//jezeli obiekt to przeciwnik skaluj prędkość co do poziomu trudności
		if (rb.tag == "Enemy") 
		{
			//przeciwnicy mają ujemny wektor prędkości z, dlatego odejmujemy
			Vector3 levelSpeed = new Vector3 (0.0f, 0.0f, 2 * (GameSettings.levelGame - 2));
			rb.velocity -= levelSpeed ;
		}
	}
		
}
