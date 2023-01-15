using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuScript_ : MonoBehaviour
{
    int currentLevel;
    int totalLevel = 8;

    private void Start() {
        currentLevel = PlayerPrefs.GetInt("Level");
    }
    public void PlayNextGame()
    {
        int newLevel = currentLevel+1;
        if(currentLevel == 7)
        {
            newLevel = 0;
        } 
        Debug.Log(newLevel);
        PlayerPrefs.SetInt("Level", newLevel);
        PlayGame();
    }
    public void PlayPrevGame()
    {
        int newLevel = currentLevel-1;
        if(currentLevel == 0)
        {
            newLevel = 0;
        } 
        Debug.Log(newLevel);
        PlayerPrefs.SetInt("Level", newLevel);
        PlayGame();
    }

    void PlayGame()
    {
        SceneManager.LoadScene("PuzzleWudhu");
    }
    public void PlayNewGame()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayGame();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
