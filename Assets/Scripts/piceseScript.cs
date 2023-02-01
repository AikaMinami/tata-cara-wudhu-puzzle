using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piceseScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    private Lean.Touch.LeanDragTranslate _leanDragTranslate;
    private Lean.Touch.LeanSelectableByFinger _leanSelectableByFinger ;
    [SerializeField]
    private bool isCalled = false;
    [SerializeField]
    private bool isWronglyPlaced = false;
    bool isThisLevelFinished;
    void Start()
    {
        RightPosition = transform.position;
        isThisLevelFinished = Camera.main.GetComponent<menuScript_>().GetPuzzleIsFinish(PlayerPrefs.GetInt("Level"));
        Debug.Log("Is this level finished "+isThisLevelFinished);
        _leanDragTranslate = this.gameObject.GetComponent<Lean.Touch.LeanDragTranslate>();
        _leanSelectableByFinger = this.gameObject.GetComponent<Lean.Touch.LeanSelectableByFinger>();
        if(!isThisLevelFinished)
        {
            transform.position = new Vector3(Random.Range(5.5f, 7.25f), Random.Range(-1.5f, 1.5f));
        } else 
        {
            _leanDragTranslate.enabled = false;
        }
    }
    
    void Update()
    {
        if(!isThisLevelFinished)
        {
            CheckDistance();
        }
    }

    void ReduceOutside()
    {
        if(isWronglyPlaced)
        {
            Camera.main.GetComponent<DragAndDrop_>().OutplacedPieces--;
            isWronglyPlaced = false;
        }
    }

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                if (InRightPosition == false)
                {
                    transform.position = RightPosition;
                    _leanDragTranslate.enabled = false;
                    Camera.main.GetComponent<DragAndDrop_>().PlaySFXDrop();
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<DragAndDrop_>().PlacedPieces++;
                    ReduceOutside();
                }
            } 
        } 
        else if(Vector3.Distance(transform.position, RightPosition) < 4f && !isCalled && !_leanSelectableByFinger.IsSelected)
        {
            if (!Selected)
            {
                Camera.main.GetComponent<DragAndDrop_>().OutplacedPieces++;
                isCalled = true;
                isWronglyPlaced = true;
            }
        }
    }
}
