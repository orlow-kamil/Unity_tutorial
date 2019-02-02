using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

	private Rigidbody rb;
	//wartość upadku
	public float tumble;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		//ustawia predkośc kątrową wzgledem wartości losowego punktu w sferze o promieniu 1
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
