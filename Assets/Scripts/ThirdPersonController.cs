using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour 
{
	public float moveSpeed;
	public float turnSpeed;

	private Transform myTransform;
	
	void Start ()
	{
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal") * turnSpeed * Time.deltaTime;
		myTransform.Rotate (0, horizontal, 0);

		float vertical = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;
		myTransform.Translate (0, 0, vertical);
	
	}
}
