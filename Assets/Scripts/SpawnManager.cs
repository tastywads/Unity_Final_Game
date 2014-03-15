using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
	public GameObject[] spawns;
	public Material[] materials;
	public GameplayManager gameplayScript;

	void Start ()
	{
		//spawns = GameObject.FindGameObjectsWithTag("SpawnSpot");
	}

	public void SpawnPlayer()
	{
		if(spawns == null)
		{
			Debug.Log("NO SPAWNS?!?!?!");
			return;
		}

		Debug.Log ("gameplayScript.GetPlayerNum() = " + gameplayScript.GetPlayerNum());
		int index = gameplayScript.GetPlayerNum() - 1;
		Debug.Log (index);
		GameObject mySpawn = spawns[index];
		GameObject myPlayer = PhotonNetwork.Instantiate("Player", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
		GameObject myPlayerObject = myPlayer.transform.Find("PlayerObject").gameObject;
		GameObject myPlayerCamera = myPlayer.transform.Find("PlayerCamera").gameObject;

		myPlayer.GetComponent<PlayerController>().enabled = true;
		myPlayerObject.GetComponent<Renderer>().material = materials[index];
		myPlayerCamera.transform.position = new Vector3(0.0f, 15.0f, -21.0f);
		myPlayerCamera.GetComponent<Camera>().enabled = true;
		myPlayerCamera.GetComponent<GUILayer>().enabled = true;
		myPlayerCamera.GetComponent<AudioListener>().enabled = true;
		myPlayerCamera.GetComponent<LookAtCamera>().enabled = true;
	}
}
