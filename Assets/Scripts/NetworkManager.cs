using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	public Camera menuCamera;
	public SpawnManager spawnScript;

	//Start server
	public void ConnectToServer(string gameTypeName)
	{
		Debug.Log("Starting a new server...");
		/*
		Network.InitializeServer (2, 25002, false);
		MasterServer.RegisterHost(gameTypeName, gameName);*/
		PhotonNetwork.ConnectUsingSettings (gameTypeName);
	}

	public void MakeRoom(string gameName)
	{
		PhotonNetwork.CreateRoom(gameName);
	}

	//Refresh server list
	public RoomInfo[] GetRoomList()
	{
		Debug.Log("Refreshing...");
		//MasterServer.RequestHostList(gameTypeName);
		return PhotonNetwork.GetRoomList();
	}

	public void JoinRoom(string gameName)
	{
		PhotonNetwork.JoinRoom(gameName);
	}

	private void OnJoinedRoom()
	{
		SpawnPlayer();
		menuCamera.enabled = false;
	}

	private void SpawnPlayer()
	{
		spawnScript.SpawnPlayer();
	}
}
