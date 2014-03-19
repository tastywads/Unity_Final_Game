using UnityEngine;
using System.Collections;

public class DeathChecker : MonoBehaviour 
{
	public PlayerData playerScript;

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("DeadTrigger"))
		{
			//Player hit trigger
			playerScript.Died();
		}
	}
}
