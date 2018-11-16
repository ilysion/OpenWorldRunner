using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject options;
    public GameObject credits;
    public Button backButton;

    public void playButton()
    {
        SceneManager.LoadScene(1);
    }

    public void optionsButton()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void creditsButton()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void exitButton()
    {
        Application.Quit();
    }
	
    public void backToMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
