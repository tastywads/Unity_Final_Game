using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float moveSpeed;
	public float turningSpeed;

	void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 force = new Vector3(x, 0, z);
		rigidbody.AddForce(force * moveSpeed);
	}
}
