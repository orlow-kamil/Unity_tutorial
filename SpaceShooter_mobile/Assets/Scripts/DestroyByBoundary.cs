using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour 
{
	//funkcja z efektem, kiedy obiekt wychodzi z kolizji innego obiektu
	void OnTriggerExit(Collider other)
	{
		// Zniszcz obiekt wychodzący
		Destroy(other.gameObject);
	}

}
