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
		//The game is on menu screen
		if(onMenu)
		{
			//Not on list of games screen
			if(connectGame == false)
			{
				//Host Game button
				if(GUI.Button(new Rect( (Screen.width/5)-75, (Screen.height/2)-50, 150, 100), "Host Game"))
				{
					hostGame = true;
				}

				//Go to the list of games screen
				if(GUI.Button(new Rect( (Screen.width/2)-75, (Screen.height/5)-50, 150, 100), "Connect to Game"))
				{
					//Get all the hosted games
					networkScript.RefreshHostList(gameTypeName);
					//Gets rid of "Enter Game Name" box
					hostGame = false;
					//Head to list of games screen
					connectGame = true;
				}

				//Host Game button is pushed
				if(hostGame == true)
				{
					//"Enter Game Name" box
					GUI.Box(new Rect( (Screen.width/2)-125, ((Screen.height/4)*3)-50, 250, 100), "Please enter game name:");

					//Take an event that just happened (in this case, we're looking for keyboard click)
					Event e = Event.current;
					//Checks if the event is "Enter" key
					if(e.keyCode == KeyCode.Return && returnButtonPressed == false)
					{
						returnButtonPressed = true;
						//Starts and hosts game
						networkScript.StartServer(gameTypeName, gameName);
						//Changes camera
						MenuCamera.depth=-1;
						//No longer in menu
						onMenu = false;
					}
					else
					{
						//Enter Name textbox
						gameName = GUI.TextField(new Rect( (Screen.width/2)-100, ((Screen.height/4)*3)-10, 200, 20), gameName, 25);
					}
				}
			}
			//On list of games screen
			else
			{
				//If there are games
				if(hostData != null)
				{
					//Print them in a list
					for(int i=0; i<hostData.Length; ++i)
					{
						//If respective game button is clicked
						if(GUI.Button(new Rect((Screen.width/3)-150, 65f + (30f * i), 300f, 30f), hostData[i].gameName))
						{
							//Connects to game
							Network.Connect(hostData[i]);
							Debug.Log("Connecting...");
							//Changes camera
							MenuCamera.depth=-1;
							//No longer on menu screens
							onMenu = false;
						}
					}
				}

				//Back button
				if(GUI.Button(new Rect( ((Screen.width/4)*3)-75, ((Screen.height/3)*2)-30, 150f, 60f), "Back"))
				{
					//No longer of list of games screen
					connectGame = false;
				}

				//Refresh button
				if(GUI.Button(new Rect( ((Screen.width/4)*3)-75, (Screen.height/3)-30, 150f, 60f), "Refresh"))
				{
					//Get all the hosted games
					networkScript.RefreshHostList(gameTypeName);
				}
			}
		}
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		switch(msEvent)
		{
		//If a list of games is recieved
		case MasterServerEvent.HostListReceived:
			{
				//Localize list of games
				hostData = MasterServer.PollHostList();
				/*
				if(hostData == null || hostData.Length == 0)
				{
					Debug.Log ("No active servers have been found.");
				}
				else
				{
					Debug.Log(hostData.Length + " server(s) found.");
				}
				*/
			}
			break;
		}
	}

	//Public function to send you back to menu
	public void OnMenu()
	{
		//On menu screen
		onMenu = true;
		//Changes back to menu camera
		MenuCamera.depth=1;
	}
}
