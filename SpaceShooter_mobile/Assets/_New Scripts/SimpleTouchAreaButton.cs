using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	//zmienne odpowiedzialna za dotyk wiekszej ilości palców niz 1
	private bool touched;
	private int pointerID;

	//zmienna odpowiedzialna za strzał 
	private bool canFire;

	void Awake () {
		touched = false;
	}

	public void OnPointerDown (PointerEventData data) {
		// Ustaw punkt startowy
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			canFire = true;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		// Zresetuj wszystko
		if (data.pointerId == pointerID) {
			canFire = false;
			touched = false;
		}
	}

	public bool CanFire () {
		return canFire;
	}
}
