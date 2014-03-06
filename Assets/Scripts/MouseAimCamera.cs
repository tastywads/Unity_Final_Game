using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {

	public GameObject target;
	public float rotateSpeed;
	Vector3 offset;

	void Start () 
	{
		// set the offset of the camera
		offset = target.transform.position - transform.position;
	}

	void Update () 
	{
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		target.transform.Rotate (0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);
		
		transform.LookAt(target.transform);

		// note:
		/*this camera has no vertial movemnt*/
		
	}
}
