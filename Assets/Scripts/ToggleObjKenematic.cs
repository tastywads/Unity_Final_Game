using UnityEngine;
using System.Collections;

public class ToggleObjKenematic : MonoBehaviour {

	public GameObject target;
	
	void Start()
	{
		target.rigidbody.useGravity = false;
		target.rigidbody.isKinematic = true;
	}
	void Update()
	{
		// for testing
		/*if (Input.GetMouseButtonDown(0)) 
		{
			ToggleGravity();
		}*/
	}
	
	public void ToggleGravity()
	{
		if (target.rigidbody.isKinematic == true) 
		{
			target.rigidbody.isKinematic = false;
			target.rigidbody.useGravity = true;
			return;
		}
		target.rigidbody.isKinematic = true;
		target.rigidbody.useGravity = false;
	}
}
