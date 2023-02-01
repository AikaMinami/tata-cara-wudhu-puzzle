using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DragAndDrop_ : MonoBehaviour
{
    public Sprite[] Levels;
    public menuScript_ menuscript_;
    public AudioSource completedAudio;
    public AudioSource tryAgainAudio;
    public AudioSource SFXPuzzle;
    public GameObject GameCompletePanel;
    public Text GameCompleteNameText;
    public int pieceAmount =4;
    public GameObject hintImage;
    int OIL = 1;    
    int currentLevel;
    public int PlacedPieces = 0;
    public int OutplacedPieces = 0;
    public ParticleSystem[] fireworks;
    bool isCalled = false;
    void Start()
    {
        GameCompletePanel.SetActive(false);
        PlacedPieces = OutplacedPieces = 0;
        Debug.Log("Current Level:" + PlayerPrefs.GetInt("Level"));
        currentLevel = PlayerPrefs.GetInt("Level");
        for (int i = 0;i < pieceAmount; i++)
        {
            GameObject.Find("Puzzle Piece").transform.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[currentLevel];
            hintImage.GetComponent<SpriteRenderer>().sprite = Levels[currentLevel];
        }
        
    }

    void Update()
    {         
        if (PlacedPieces == pieceAmount)
        {
            completedAudio.Play();
            menuscript_.SetPuzzleIsFinish(currentLevel);
            Debug.Log("is level " + currentLevel + " finished = " + menuscript_.GetPuzzleIsFinish(currentLevel));
            PlacedPieces = 0;
            if(menuscript_.IsAllIndexTrue())
            {
                menuscript_.PauseTimer();
                StartCoroutine(GameComplete());
            }
        } 
        if((OutplacedPieces+PlacedPieces) == pieceAmount && OutplacedPieces > 0){
            if(PlacedPieces < pieceAmount && !isCalled)
            {
                tryAgainAudio.Play();
                isCalled = true;
            }
        }
    }

    void ActivateFirework()
    {
        for(int i=0;i<fireworks.Length;i++)
        {
            fireworks[i].Play();
        }
    }

    IEnumerator GameComplete()
    {
        ActivateFirework();
        yield return new WaitForSeconds(3f);
        OpenGameCompletePanel();
        PlayerPrefs.SetInt("Level", 0);
    }
    void OpenGameCompletePanel()
    {
        menuscript_.SetMedal();
        GameCompleteNameText.text = "Selamat, " + PlayerPrefs.GetString("PlayerName") + "!";
        GameCompletePanel.SetActive(true);
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        SceneManager.LoadScene("Game");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlaySFXDrop()
    {
        SFXPuzzle.Play();
    }
}

