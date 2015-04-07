using UnityEngine;
using System.Collections;

[System.Serializable] //serialising so that unity have connection to boundary
[RequireComponent(typeof(SphereCollider))]


public class Boundary //putting the variable in a class of their own, så that they can be used by other scripts. the class will not inherit from anything
{
	public float xMin = -6.77f;
	public float yMax = 4.0f; 
	public float xMax = 6.38f;
	public float yMin = 0.48f;
 
}

public class MouseInteraction : MonoBehaviour 
{
	public Boundary boundary;//instance of Boundary
	private Vector3 screenPoint;
	private Vector3 offset;

	//should restrict the object within this field
	  

	public void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenPoint.z));
		
	}
	
	public void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		transform.position = curPosition;

		//should restrict the object within this field
		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x,boundary.xMin,boundary.xMax),
			Mathf.Clamp (rigidbody.position.y,boundary.yMin,boundary.yMax),
			-1.43f
			); 


	
		
	}
	
}
