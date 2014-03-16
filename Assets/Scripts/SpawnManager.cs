using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
	public GameObject[] spawns;
	public GameplayManager gameplayScript;

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

		GameObject myPlayer;
		switch(index)
		{
		case 0:
			myPlayer = PhotonNetwork.Instantiate("Player1", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
			break;
		case 1:
			myPlayer = PhotonNetwork.Instantiate("Player2", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
			break;
		case 2:
			myPlayer = PhotonNetwork.Instantiate("Player3", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
			break;
		case 3:
			myPlayer = PhotonNetwork.Instantiate("Player4", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
			break;
		default:
			myPlayer = PhotonNetwork.Instantiate("Player1", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
			break;
		}

		GameObject myPlayerObject = myPlayer.transform.Find("PlayerObject").gameObject;
		GameObject myPlayerCamera = myPlayer.transform.Find("PlayerCamera").gameObject;
		PlayerController myPlayerController = myPlayer.GetComponent<PlayerController>();

		gameplayScript.playerControllerScripts.Add (myPlayerController);
		myPlayerCamera.transform.position = new Vector3(0.0f, 15.0f, -21.0f);
		myPlayerCamera.GetComponent<Camera>().enabled = true;
		myPlayerCamera.GetComponent<GUILayer>().enabled = true;
		myPlayerCamera.GetComponent<AudioListener>().enabled = true;
		myPlayerCamera.GetComponent<LookAtCamera>().enabled = true;
	}
}
