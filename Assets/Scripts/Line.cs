using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;

	[HideInInspector] public List<Vector2> points = new List<Vector2> ( );
	[HideInInspector] public int pointsCount = 0;

	//The minimum distance between line's points.
	float pointsMinDistance = 0.1f;

	//Circle collider added to each line's point

	public void AddPoint ( Vector3 newPt ) {
		Debug.Log("add");
		Vector2 newPoint = new Vector2(newPt.x, newPt.y);
		//If distance between last point and new point is less than pointsMinDistance do nothing (return)
		if ( pointsCount >= 1 && Vector2.Distance ( newPoint, GetLastPoint ( ) ) < pointsMinDistance )
			return;

		points.Add ( newPoint );
		pointsCount++;

		//Line Renderer
		lineRenderer.positionCount = pointsCount;
		lineRenderer.SetPosition ( pointsCount - 1, newPoint );
	}

	public Vector2 GetLastPoint ( ) {
		return ( Vector2 )lineRenderer.GetPosition ( pointsCount - 1 );
	}

	// public void UsePhysics ( bool usePhysics ) {
	// 	// isKinematic = true  means that this rigidbody is not affected by Unity's physics engine
	// 	rigidBody.isKinematic = !usePhysics;
	// }

	public void SetLineColor ( Gradient colorGradient ) {
		lineRenderer.colorGradient = colorGradient;
	}

	public void SetPointsMinDistance ( float distance ) {
		pointsMinDistance = distance;
	}

	public void SetLineWidth ( float width ) {
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
	}
}
