﻿using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
	private GameObject[] spawns;

	void Start ()
	{
		spawns = GameObject.FindGameObjectsWithTag("SpawnSpot");
	}

	public void SpawnPlayer()
	{
		if(spawns == null)
		{
			Debug.Log("NO SPAWNS?!?!?!");
			return;
		}

		GameObject mySpawn = spawns[Random.Range(0,spawns.Length)];
		GameObject myPlayer = PhotonNetwork.Instantiate("Player", mySpawn.transform.position, mySpawn.transform.rotation, 0) as GameObject;
		GameObject myPlayerCamera = myPlayer.transform.Find("PlayerCamera").gameObject;

		myPlayer.GetComponent<PlayerController>().enabled = true;
		Vector3 setposition = new Vector3(0.0f, 12.0f, -19.0f);
		myPlayerCamera.GetComponent<Transform>().position = setposition;
		myPlayerCamera.GetComponent<Camera>().enabled = true;
		myPlayerCamera.GetComponent<GUILayer>().enabled = true;
		myPlayerCamera.GetComponent<AudioListener>().enabled = true;
		myPlayerCamera.GetComponent<LookAtCamera>().enabled = true;
	}
}