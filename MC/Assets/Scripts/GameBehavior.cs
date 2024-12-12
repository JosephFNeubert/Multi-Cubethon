using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    // Game Manager variables
    bool gameEnded = false;
    public float restartDelay = 1f;
    public GameObject CompleteLevelUI;

    // Check for completed level
    public void completeLevel()
    {
        CompleteLevelUI.SetActive(true);
    } 

    // End the game
    public void EndGame()
    {
        if(gameEnded == false)
        {
            gameEnded = true;
            Invoke("Restart", restartDelay);
        }

    }

    // Restart function
    void Restart()
    {
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
