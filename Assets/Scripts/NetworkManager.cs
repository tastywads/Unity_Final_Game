using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	//Start server
	public void StartServer(string gameTypeName, string gameName)
	{
		Debug.Log("Starting a new server...");
		Network.InitializeServer (2, 25002, false);
		MasterServer.RegisterHost(gameTypeName, gameName);
	}

	//Refresh server list
	public void RefreshHostList(string gameTypeName)
	{
		Debug.Log("Refreshing...");
		MasterServer.RequestHostList(gameTypeName);
	}
}
