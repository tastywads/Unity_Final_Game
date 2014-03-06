using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float moveSpeed;
	public float turningSpeed;

	void Update ()
	{
		float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
		transform.Rotate(0,horizontal, 0);

		float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		transform.Translate(0, 0, vertical);
	}
}
