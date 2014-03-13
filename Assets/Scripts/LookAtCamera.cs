using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour 
{
	/*This Script is for a still camera to focus on a gameObject*/
	public Transform target;
	public float damping;

	// late update is called after all other update functions
	void LateUpdate() 
	{
		// larping the camera angle movement to smoth it out
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
		Quaternion rotation = Quaternion.Euler(0, angle, 0);

		// move the camera
		transform.LookAt (target);
	}
}
