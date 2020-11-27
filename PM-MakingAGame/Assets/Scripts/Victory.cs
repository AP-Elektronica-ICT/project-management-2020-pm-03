using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("victory");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
        FindObjectOfType<AudioManager>().Play("level1");
        FindObjectOfType<AudioManager>().Stop("victory");
        Scorescript.ScoreValue = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
        FindObjectOfType<AudioManager>().Stop("victory");
        SceneManager.LoadScene("Menu");
    }
}
