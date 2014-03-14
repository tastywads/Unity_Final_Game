using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public Rigidbody myRigidbody;

    public float cooldownLength;
	public float moveSpeed;
    public float dashStrength;

    private float cooldown;

    void Start()
    {
        cooldown = cooldownLength;
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;   
        }
    }

	void FixedUpdate()
	{
        // movement
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 force = new Vector3(x, 0, z);
		myRigidbody.AddForce(force * moveSpeed);

        //player dash
        if (  Input.GetKeyDown(KeyCode.Space) && cooldown <= 0) 
		{
            Debug.Log("Player Dash");
            myRigidbody.AddForce(force * dashStrength, ForceMode.Impulse);
            cooldown = cooldownLength;
		}
	}
}
