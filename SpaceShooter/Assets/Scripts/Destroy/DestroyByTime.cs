using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifeTime;

	void Start () 
	{
		//funkcja niszcząca obiekt po jego czasie życia
		Destroy (gameObject, lifeTime);
	}

}
