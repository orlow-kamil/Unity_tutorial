using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//polecenie, aby zadeklarowana klasa była widoczna w Inspektorze
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{

	private Rigidbody rb;
	private AudioSource aus;

	//zmienne dostępne w Inspektorze
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;  //zadeklarowany typ obiektu -> wziete tylko transform obiektu

	public float fireRate;

	//zmienne pol dotykowych
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;

	//zmienne dostepne tylko w skrypcie
	private float nextFire;
	private Quaternion calibrateQuaterion; //quaternion do trzymania aktualnej rotacji

	//funckja wczytywana podczas startu gry, ustawienia początkowe parametrów
	void Start()
	{
		//obiekt posiada właściwości ciała stałego
		rb = GetComponent<Rigidbody> ();
		//obiekt posiada dźwiek
		aus = GetComponent<AudioSource> ();

		CalibrateAccellerometer ();
	}
	//funkcja zapętlona dla każdego kadru gry
	void Update()
	{
		//warunek nacisniecie prawego przycisku pola i odczekania na ponowne załadowanie broni (warunek czasowy)
		if (areaButton.CanFire() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			//funkcja klonujaca obiekt i zwracajaca klona jako obiekt (obiekt oryginalny, pozycja klona, rotacja klona)
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			//uruchomienie dźwieku strzału
			aus.Play();
		}
	}

	//funkcja do zapisania danych z akcelerometru -> zrzut danych
	void CalibrateAccellerometer ()
	{
		//wektor przyspieszenia g <Translacja>
		Vector3 accelerationSnapshot = Input.acceleration;
		//quaterion przyspieszenia g <Rotacja> -> [0,0,-1] + translacja
		Quaternion rotateQuaterion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		//skalibrowany quaterion przyspeszenia g -> inwersja rotateQuaterion 
		calibrateQuaterion = Quaternion.Inverse (rotateQuaterion); 

	}

	//funckcja do kalibracji na bazie danych z funkcji CalibrateAccellerometer -> poprawa kalibracji wstepnej Offset
	Vector3 FixAccelleretion (Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrateQuaterion * acceleration;
		return fixedAcceleration;
	}

	//funkcja zapetlona dla fizyki obiektów
	void FixedUpdate()
	{
		//wektor akceleracji (surowa)
//		Vector3 accelerationRaw = Input.acceleration;
		//wektor akceleracji (końcowa)
//		Vector3 acceleration = FixAccelleretion (accelerationRaw);

		//wektor kierunku -> wartość z klasy touchPad
		Vector2 direction = touchPad.GetDirection ();
		//wektor prędkości -> brak ruchu góra/dół (specyfikacja gier typu 2D)
		Vector3 movement =  new Vector3 (direction.x, 0.0f, direction.y);
		//mnoznik z powodu fabrycznego ustawienia prędkości rzędu 1 j/s
		rb.velocity = movement * speed;

		//ograniczenia sceny -> ustalenie wektora położenia i rotacji
		rb.position = new Vector3 
		(
			//wartość między min a max w położeniu x,z
			Mathf.Clamp (rb.position.x , boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z , boundary.zMin, boundary.zMax)
		);

		//ochylenie podczas poruszania prawo/lewo przeciwnie do ruchu
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}


}
