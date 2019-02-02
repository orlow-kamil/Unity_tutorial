using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	//stworzenie obiektu GameController -> dostęp do skrytpu z innego skryptu
	private GameController gameController;


	void Start ()
	{
		//Przypisanie do czystego obiektu obiekt z tagiem GameController
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			//przypisanie z komponenetu -> jedyne rozwiazanie problemu
			gameController = gameControllerObject.GetComponent <GameController> ();
		}

		//jezeli by źle przepisało
		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	//funkcja z efektem, kiedy obiekt wchodzi z kolizją innego obiektu
	void OnTriggerEnter(Collider other) 
	{
		//jezeli to granica lub przeciwnik, to zakończ funkcję kolizji brakiem zmian
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") )
		{
			return;
		}

		//jezeli jest eksplozja -> mozliwość nie wstawienia referencji do obiektu (pomijamy kod)
		if (explosion != null) 
		{
			//tworzenie klona obiektu eksplozja w miejscu zniszczonego obiektu
			Instantiate (explosion, transform.position, transform.rotation);
		}

		//jezeli obiekt to gracz
		if (other.CompareTag("Player")) 
		{
			//tworzenie klona eksplozji gracza w miejscu, w którym gracz był
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			//referencja do funkcji z innego skryptu -> koniec gry
			gameController.GameOver ();
		}

		//referencja do funkcji z innego skryptu -> liczenie punktów za zniszczenie asteroidy
		gameController.AddScore (scoreValue);
		//zniszcz obiekt, który wejdzie w kolizje
		Destroy(other.gameObject);
		//zniszcz ten obiekt
		Destroy(gameObject);
	}
}
