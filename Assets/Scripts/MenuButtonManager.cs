using UnityEngine;
using System.Collections;

public class MenuButtonManager : MonoBehaviour 
{
	public NetworkManager networkScript;
	public Camera MenuCamera;
	public Camera BoardCamera;

	private string gameName;
	private string gameTypeName;
	private bool onMenu;
	private bool hostGame;
	private bool connectGame;
	private bool returnButtonPressed;
	private HostData[] hostData;

	void Awake()
	{
		gameName = "";
		gameTypeName = "Future_Card_Buddyfight_Online";
		hostGame = false;
		connectGame = false;
		returnButtonPressed = false;
		hostData = null;
		onMenu = true;
	}

	void OnGUI()
	{
		if(onMenu)
		{
			if(connectGame == false)
			{
				if(GUI.Button(new Rect( (Screen.width/5)-75, (Screen.height/2)-50, 150, 100), "Host Game"))
				{
					hostGame = true;
				}
				
				if(GUI.Button(new Rect( (Screen.width/2)-75, (Screen.height/5)-50, 150, 100), "Connect to Game"))
				{
					networkScript.RefreshHostList(gameTypeName);
					hostGame = false;
					connectGame = true;
				}

				if(hostGame == true)
				{
					GUI.Box(new Rect( (Screen.width/2)-125, ((Screen.height/4)*3)-50, 250, 100), "Please enter game name:");

					Event e = Event.current;
					if(e.keyCode == KeyCode.Return && returnButtonPressed == false)
					{
						Debug.Log("Return");
						returnButtonPressed = true;
						networkScript.StartServer(gameTypeName, gameName);
						MenuCamera.depth=0;
						onMenu = false;
					}
					else
					{
						gameName = GUI.TextField(new Rect( (Screen.width/2)-100, ((Screen.height/4)*3)-10, 200, 20), gameName, 25);
					}
				}
			}
			else
			{
				if(hostData != null)
				{
					for(int i=0; i<hostData.Length; ++i)
					{
						if(GUI.Button(new Rect((Screen.width/3)-150, 65f + (30f * i), 300f, 30f), hostData[i].gameName))
						{
							Network.Connect(hostData[i]);
							Debug.Log("Connecting...");
							MenuCamera.depth=0;
							onMenu = false;
						}
					}
				}
			}
		}
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		switch(msEvent)
		{
		case MasterServerEvent.HostListReceived:
			{
				hostData = MasterServer.PollHostList();
				if(hostData == null || hostData.Length == 0)
				{
					Debug.Log ("No active servers have been found.");
				}
				else
				{
					Debug.Log(hostData.Length + " server(s) found.");
				}
			}
			break;
		}
	}

	public void OnMenu()
	{
		onMenu = true;
	}
}
