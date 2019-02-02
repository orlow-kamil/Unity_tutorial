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

	private float nextFire;

	//funckja wczytywana podczas startu gry, ustawienia początkowe parametrów
	void Start()
	{
		//obiekt posiada właściwości ciała stałego
		rb = GetComponent<Rigidbody> ();
		//obiekt posiada dźwiek
		aus = GetComponent<AudioSource> ();
	}
	//funkcja zapętlona dla każdego kadru gry
	void Update()
	{
		//warunek nacisniecie prawego przycisku myszki i odczekania na ponowne załadowanie broni (warunek czasowy)
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			//funkcja klonujaca obiekt i zwracajaca klona jako obiekt (obiekt oryginalny, pozycja klona, rotacja klona)
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			//uruchomienie dźwieku strzału
			aus.Play();
		}
	}

	//funkcja zapetlona dla fizyki obiektów
	void FixedUpdate()
	{
		//ruch poziomy prawo/lewo
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//ruch pionowy przód/tył
		float moveVertical = Input.GetAxis ("Vertical");

		//wektor prędkości -> brak ruchu góra/dół (specyfikacja gier typu 2D)
		Vector3 movement =  new Vector3 (moveHorizontal, 0.0f, moveVertical);
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
