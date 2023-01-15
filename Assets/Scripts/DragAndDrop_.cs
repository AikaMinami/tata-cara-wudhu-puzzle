using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class DragAndDrop_ : MonoBehaviour
{
    public Sprite[] Levels;

    public AudioSource completedAudio;
    public GameObject SelectedPiece;
    public int pieceAmount =4;
    public GameObject hintImage;
    int OIL = 1;    
    int currentLevel;
    public int PlacedPieces = 0;
    void Start()
    {
        Debug.Log("Current Level:" + PlayerPrefs.GetInt("Level"));
        currentLevel = PlayerPrefs.GetInt("Level");
        for (int i = 0;i < pieceAmount; i++)
        {
            // GameObject.FindGameObjectWithTag("ImageMask").GetComponent<SpriteRenderer>().sprite = Levels[currentLevel];
            GameObject.Find("Puzzle Piece").transform.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[currentLevel];
            hintImage.GetComponent<SpriteRenderer>().sprite = Levels[currentLevel];
        }
        
    }

    void Update()
    {
        // bool fal = false;
        // if (Input.GetMouseButtonDown(0) && fal)
        // {
        //     RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //     if (hit.transform.CompareTag("Puzzle"))
        //     {
        //         if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
        //         {
        //             SelectedPiece = hit.transform.gameObject;
        //             SelectedPiece.GetComponent<piceseScript>().Selected = true;
        //             SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
        //             OIL++;
        //         }
        //     } 
        //     else 
        //     {
        //         Debug.Log("dont do anything");
        //     }
        // }

        // if (Input.GetMouseButtonUp(0) && fal)
        // {
        //     if (SelectedPiece != null)
        //     {
        //         SelectedPiece.GetComponent<piceseScript>().Selected = false;
        //         SelectedPiece = null;
        //     }
        // }
        // if (SelectedPiece != null && fal)
        // {
        //     Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     SelectedPiece.transform.position = new Vector3(MousePoint.x,MousePoint.y,0);
        // }             
        if (PlacedPieces == pieceAmount)
        {
            completedAudio.Play();
            PlacedPieces = 0;
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
}