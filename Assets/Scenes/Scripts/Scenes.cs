using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // function to start the game
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    //function to exit the game
    public void ExitGame() {
        Application.Quit();
    }

    // function to reload the game scene
    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
