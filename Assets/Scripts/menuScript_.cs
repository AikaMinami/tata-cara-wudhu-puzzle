using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuScript_ : MonoBehaviour
{
    int currentLevel;
    static int totalLevel = 8;
    public static bool[] isDone = new bool[totalLevel];
    public static float timer;
    private float _expectedTimePerfect = 60f, _expectedTimeVeryGood = 120f, _expectedTimeGood = 180f;
    public Text playerName, welcomeText, alertText, placeholderText, timerText;
    public GameObject setNamePanel, starPrefab;
    public Transform starParent;
    public Button PrevButton, NextButton;
    private bool _isTimerPaused;
    public Sprite[] medalSprite;
    public Image medalImage;

    private void Start() {
        currentLevel = PlayerPrefs.GetInt("Level");
        StartTimer();
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            setNamePanel.SetActive(false);

            SetWelcomeText();   
            if(PlayerPrefs.GetString("PlayerName") == "")
            {
                setNamePanel.SetActive(true);
            } 
        } else if(SceneManager.GetActiveScene().name == "PuzzleWudhu" )
        {
            ButtonCheck();
        }
    }

    private void Update() {
        if(SceneManager.GetActiveScene().name == "PuzzleWudhu" && _isTimerPaused)
        {
            timer+=Time.deltaTime;
            int minutes = ((int)timer)/60;
            int seconds = ((int)timer)%60;
            timerText.text = minutes+":"+seconds;
        }
    }

    void ButtonCheck()
    {
        if(currentLevel == 7)
        {
            NextButton.interactable = false;
        } else if(currentLevel == 0)
        {
            PrevButton.interactable = false;
        }
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
        SetAllIndexFalse();
        timer = 0;
        PlayGame();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.LoadSceneAsync("");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetPlayerName()
    {
        if(playerName.text == "")
        {
            StartCoroutine(ShowAlert());
            return;
        }
        PlayerPrefs.SetString("PlayerName", playerName.text);
        SetWelcomeText();
        setNamePanel.SetActive(false);
    }

    void SetWelcomeText()
    {
        welcomeText.text = "Selamat Datang, " + PlayerPrefs.GetString("PlayerName");
    }

    IEnumerator ShowAlert()
    {
        alertText.text = "Nama Tidak Boleh Kosong Ya";
        yield return new WaitForSeconds(2);
        alertText.text = "";
    }

    public void SetPuzzleIsFinish(int idx)
    {
        isDone[idx] = true;
    }
    public bool GetPuzzleIsFinish(int idx)
    {
        return isDone[idx];
    }

    public bool IsAllIndexTrue()
    {
        for(int i=0;i<isDone.Length;i++)
        {
            if(!isDone[i]) return false;
        }
        return true;
    }

    void SetAllIndexFalse()
    {
        for(int i=0;i<isDone.Length;i++)
        {
            isDone[i] = false;
        }
        
    }

    public void StartTimer()
    {
        _isTimerPaused = true;
    }

    public void PauseTimer()
    {
        _isTimerPaused = false;
    }

    public void SetMedal()
    {
        if(timer<_expectedTimePerfect)
        {
            InstantiateStars(4);
        } 
        else if(timer<_expectedTimeVeryGood)
        {
            InstantiateStars(3);
        } 
        else if(timer<_expectedTimeGood)
        {
            InstantiateStars(2);
        } 
        else
        {
            InstantiateStars(1);
        }
    }

    void InstantiateStars(int amt)
    {
        for(int i=0;i<amt;i++)
        {
            GameObject star = Instantiate(starPrefab, starParent.position, Quaternion.identity);
            star.transform.localScale = new Vector3(1,1,1);
            star.transform.parent = starParent;
        }
        
    }
}
