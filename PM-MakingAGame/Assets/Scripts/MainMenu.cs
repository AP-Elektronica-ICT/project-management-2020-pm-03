using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
    }



    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        FindObjectOfType<AudioManager>().Play("level1");
        Scorescript.ScoreValue = 0;


    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

