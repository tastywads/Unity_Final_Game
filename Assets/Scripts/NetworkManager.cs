using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour 
{
	public SpawnManager spawnScript;
	public GameplayManager gameplayScript;
	public MenuButtonManager menuScript;

	private PhotonView myPhotonView;
	private List<string> chatMessages;
	private int maxChatMessages;

	void Start()
	{
		myPhotonView = GetComponent<PhotonView>();
		maxChatMessages = 5;
		chatMessages = new List<string>();
	}

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
		Debug.Log ("Make Room");
		PhotonNetwork.CreateRoom(gameName,true,true,4);
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

	public void AddChatMessage(string message)
	{
		myPhotonView.RPC("AddChatMessage_RPC", PhotonTargets.AllBuffered, message);
	}

	[RPC]
	private void AddChatMessage_RPC(string message)
	{
		while(chatMessages.Count >= maxChatMessages)
		{
			chatMessages.RemoveAt(0);
		}

		chatMessages.Add(message);
	}

	void OnGUI()
	{
		if(gameplayScript.GetGameState() != GameplayManager.gameState.menu)
		{
			GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
			
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			foreach(string message in chatMessages)
			{
				GUILayout.Label (message);
			}
			
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}

	private void OnJoinedRoom()
	{
		Debug.Log ("isMessageQueue: " + PhotonNetwork.isMessageQueueRunning);
		gameplayScript.PlayerJoined();
		AddChatMessage(PhotonNetwork.player.name + " has joined the game!");
	}

	private void RoomError()
	{
		menuScript.OnMenu();
	}

	private void OnPhotonJoinRoomFailed()
	{
		RoomError();
	}

	private void OnPhotonCreateRoomFailed()
	{
		RoomError();
	}

	public void SpawnPlayer()
	{
		spawnScript.SpawnPlayer();
	}
}
