using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;

	//zmienne odpowiedzialna za dotyk wiekszej ilości palców niz 1
	private bool touched;
	private int pointerID;

	void Awake () {
		//ustawia wektor kierunku na zero
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown (PointerEventData data) {
		// Ustaw punkt startowy
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}
	}

	public void OnDrag (PointerEventData data) {
		// Porównaj różnice miedzy obecna pozycja a punktem startowym
		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			//normalizacja sprawia, że rozmiar wektora przyjmuje wartosc 1, kierunek wciąż ten sam
			direction = directionRaw.normalized;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		// Zresetuj wszystko
		if (data.pointerId == pointerID) {
			direction = Vector2.zero;
			touched = false;
		}
	}

	public Vector2 GetDirection () {
		//zwraca wektor kierunku
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}
