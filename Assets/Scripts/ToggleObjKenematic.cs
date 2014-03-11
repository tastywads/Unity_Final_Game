using UnityEngine;
using System.Collections;

public class ToggleObjKenematic : MonoBehaviour {

	public GameObject target;
    public bool playerHitToggle;
	
	void Start()
	{
		target.rigidbody.useGravity = false;
		target.rigidbody.isKinematic = true;
	}

    void OnCollisionEnter(Collision hit)
    {
        if (playerHitToggle && hit.rigidbody.CompareTag("Player"))
        {
            Debug.Log("Player hit Falling wall");
            target.rigidbody.isKinematic = false;
            target.rigidbody.useGravity = true;
        }
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
