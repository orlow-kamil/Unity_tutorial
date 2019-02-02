using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class GameSettings : MonoBehaviour {

	//zmienne statyczne
	private static bool created = false;
	private static bool choosenLevel = false;
	public static int levelGame;

	public Dropdown levelDropDown;
	public Dropdown resolutionDropdown;

	public AudioMixer audioMixer;

	//tablica parametrów ekranu (specificzny zapis) 
	Resolution[] resolutions;

	void Start()
	{
		levelDropDown.captionText.text = "SELECTION";

		//przypisanie do tablicy wszystkich dostepnych parametrów ekranu
		resolutions = Screen.resolutions;

		//wyczyszczenie obiektu ResolutionDropdown
		resolutionDropdown.ClearOptions ();

		List<string> options = new List<string> ();


		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++) 
		{
			//zapis do listy options parametrów ekranu w formie string <wysokosc x szerokosc>
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);

			//znalezienie aktualnych wymiarów i zapisanie indexu
			if (resolutions [i].width == Screen.currentResolution.width &&
			    resolutions [i].height == Screen.currentResolution.height) 
			{
				currentResolutionIndex = i;
			}
		}
		//zapis do obiektu ResolutionDropdown wszystkich elementów z listy options
		resolutionDropdown.AddOptions (options);

		//ustawienie widoku na aktualnym indexie w obiekcie ResolutionDropdown
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue ();
	}

	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this.gameObject);
			created = true;
			Debug.Log("Awake: " + this.gameObject);
		}
	}


	//MAIN MENU

	public void PlayGame() 
	{
		if (!choosenLevel)
			levelGame = 2;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitGame() 
	{
		Debug.Log ("Quit");
		Application.Quit ();
	}

	//OPTIONS MENU

	public void SetLevelGame()
	{
		levelGame = levelDropDown.value;
		choosenLevel = true;
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetFullscreen ( bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SetVolumeMusic (float volume) 
	{
		audioMixer.SetFloat ("volumeMusic", volume);
	}

	public void SetVolumeEffects (float volume) 
	{
		audioMixer.SetFloat ("volumeEffects", volume);
	}


}
