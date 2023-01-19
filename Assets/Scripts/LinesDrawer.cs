using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab;
	public LayerMask cantDrawOverLayer;
	int cantDrawOverLayerIndex;

	[Space ( 30f )]
	public Gradient lineColor;
	public float linePointsMinDistance;
	public float lineWidth;

	public List<Line> lineLists;
	// public Text texts;
	Line currentLine;

	Camera cam;
	int i = 0;

	void Start ( ) {
		cam = Camera.main;
		cantDrawOverLayerIndex = LayerMask.NameToLayer ( "CantDrawOver" );
	}

	void Update ( ) {
		Debug.Log(currentLine);
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if ( Input.GetMouseButtonDown ( 0 ) || touch.phase == TouchPhase.Began)
			{
				Debug.Log("Begin");
				BeginDraw ( );
			}
			if ( currentLine != null || touch.phase == TouchPhase.Moved )
				{Debug.Log("Process");
				Draw ( );}

			if ( Input.GetMouseButtonUp ( 0 ) || touch.phase == TouchPhase.Ended)
				{Debug.Log("End");
				EndDraw ( );}
		}
	}

	// Begin Draw ----------------------------------------------
	void BeginDraw ( ) {
		currentLine = Instantiate ( linePrefab, this.transform ).GetComponent <Line> ( );

		//Set line properties
		// currentLine.UsePhysics ( false );
		currentLine.SetLineColor ( lineColor );
		currentLine.SetPointsMinDistance ( linePointsMinDistance );
		currentLine.SetLineWidth ( lineWidth );
		lineLists.Add(currentLine);
	}
	// Draw ----------------------------------------------------
	void Draw ( ) {
		i++;
		// Vector3 screenPosition = Input.mousePosition;
		Vector3 screenPosition = Input.GetTouch(0).position;
		screenPosition.x = screenPosition.x - 0.49f;
		// screenPosition.z = Camera.main.transform.position.y - transform.position.y;
		
		screenPosition.z = Camera.main.nearClipPlane;
	

		Vector3 mousePosition = cam.ScreenToWorldPoint ( screenPosition );
		// texts.text = ""+mousePosition + "i = " + i;
		Debug.Log("Pos" + mousePosition);
		//Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
		// RaycastHit2D hit = Physics2D.CircleCast ( mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer );

		// if ( hit )
			// EndDraw ( );
		// else
		currentLine.AddPoint ( mousePosition );
	}
	// End Draw ------------------------------------------------
	void EndDraw ( ) {
		if ( currentLine != null ) {
			if ( currentLine.pointsCount < 2 ) {
				//If line has one point
				Destroy ( currentLine.gameObject );
			} else {
				//Add the line to "CantDrawOver" layer
				// currentLine.gameObject.layer = cantDrawOverLayerIndex;

				currentLine = null;
			}
		}
	}

	public void ResetLine()
	{
		foreach (Line line in lineLists)
		{
			Destroy(line.gameObject);			
		}
	
		lineLists.Clear();
	}

	public void ClearList()
	{
		lineLists.Clear();
	}
}
