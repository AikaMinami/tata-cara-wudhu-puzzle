using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class DragAndDrop_ : MonoBehaviour
{
    public Sprite[] Levels;

    public AudioSource completedAudio;
    public AudioSource tryAgainAudio;
    public AudioSource SFXPuzzle;
    public GameObject SelectedPiece;
    public int pieceAmount =4;
    public GameObject hintImage;
    int OIL = 1;    
    int currentLevel;
    public int PlacedPieces = 0;
    public int OutplacedPieces = 0;
    bool isCalled = false;
    void Start()
    {
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
            PlacedPieces = 0;
        } 
        if((OutplacedPieces+PlacedPieces) == pieceAmount && OutplacedPieces > 0){
            if(PlacedPieces < pieceAmount && !isCalled)
            {
                tryAgainAudio.Play();
                isCalled = true;
            }
        }
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

