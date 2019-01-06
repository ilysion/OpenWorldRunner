using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject options;
    public GameObject credits;
    public Dropdown dropdown;
    public Button backButton;

    public void Start()
    {
        int current = QualitySettings.GetQualityLevel();
        List<string> insertable = new List<string>();
        foreach (var x in QualitySettings.names)
            insertable.Add(x);

        dropdown.AddOptions(insertable);
        dropdown.value = current;
    }

    public void playButton()
    {
        SceneManager.LoadScene(1);
    }

    public void updateQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
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
