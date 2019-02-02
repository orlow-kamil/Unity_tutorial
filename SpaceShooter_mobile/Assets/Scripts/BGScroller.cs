using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

	public float scrollSpeed;
	public float tileSizeZ;

	private Vector3 startPosition;

	void Start () {
		//określenie pozycji startowej
		startPosition = transform.position;
	}

	void Update () {
		//nowa pozycja wyliczana z funkcji Repeat -> reszta z dzielenia modulo (typ float)
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed,tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
