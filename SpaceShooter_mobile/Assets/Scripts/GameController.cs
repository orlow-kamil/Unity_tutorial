using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//lista generowanych obiektów
	public GameObject[] hazards;
	//zmienna przechowująca parametry nowego stworzonego obiektu
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	//zmienne tekstowe
	public Text scoreText;
	public Text gameOverText;

	//zmienne niemodyfikowalane/prywatne
	private int score;
	private bool gameOver;
	public GameObject restartButton;

	//ustawienia początkowe sceny
	void Start ()
	{
		gameOver = false;
		restartButton.SetActive (false);
		//wyświetlamy nic na początku -> puste napisy
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		//rozpocznij współprogram do kreowania fal meteorytów
		StartCoroutine (SpawnWaves ());
	}
		

	//funkcja tworząca nowe meteoryty w miejscu do generowania -> tworzy je rutynowo co 1 klatke
	IEnumerator SpawnWaves ()
	{
		//początkowe czekanie na gracza
		yield return new WaitForSeconds (startWait);
		//nieskończona fala asteroid
		while(true)
		{
			for (int i = 0; i < hazardCount ; i++) 
			{
				//wybór losowego obiektu z tablicy o rozmiarze Length
				GameObject hazard = hazards[Random.Range (0,hazards.Length)];
				//parametry pozycji obiektu -> losowa wartość x z przedziału krawędzi mapy
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//bez rotacji -> idealnie dopasowany do osi globalnej badz lokalnej (rodzic)
				Quaternion spawnRotation = Quaternion.identity;
				//tworzenie klona obiektu
				Instantiate (hazard, spawnPosition, spawnRotation);
				//czekanie na kolejny obiekt
				yield return new WaitForSeconds (spawnWait);
			}
			//czekanie na kolejna fale asteroid
			yield return new WaitForSeconds (waveWait);

			//warunek końca gry
			if (gameOver) 
			{
				restartButton.SetActive (true);
				//przerwanie funkcji
				break;
			}
		}
	}

	//funkcja licząca aktualny wynik
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	//funkcja wyświetlająca aktualny wynik
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	//funkcja wyświetlajaca koniec gry
	public void GameOver ()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void RestartGame () {
		Application.LoadLevel (Application.loadedLevel);
	}
}
