using UnityEngine;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
	public enum gameState
	{
		menu,
		waiting,
		playing,
		done
	}

	public SpawnManager spawnScript;
	public NetworkManager networkScript;
	public GameManager gameScript;
	public Camera overviewCamera;

	public PlayerController playerControllerScript;
	public List<int> playerNums;

	private bool spawned;
	private bool ready;
	private bool gameStart;
	private gameState myGameState;
	private int numOfPlayers;
	private int playersRdy;
	private PhotonView myPhotonView;
	private int myPlayerNum;
	private int playersAlive;
	private bool amIAlive;

	void Awake()
	{
		numOfPlayers = 0;
		playersRdy = 0;
		playersAlive = 0;
		myGameState = gameState.menu;
		spawned = false;
		ready = false;
		gameStart = false;
		amIAlive = false;
		playerNums = new List<int>();
	}

	void Start () 
	{
		myPhotonView = GetComponent<PhotonView>();
	}

	void Update () 
	{
		if(myGameState == gameState.waiting)
		{
			if(spawned == false)
			{
				overviewCamera.enabled = false;
				networkScript.AddChatMessage(PhotonNetwork.player.name + " has joined the game!");

				int index = -1;
				bool taken = true;
				while(taken)
				{
					++index;
					taken = false;
					foreach(int playerNum in playerNums)
					{
						if(playerNum == index)
						{
							taken = true;
						}
					}
				}

				spawnScript.SpawnPlayer(index);
				spawned = true;
			}
		}
		else if(myGameState == gameState.playing)
		{
			if(gameStart == false)
			{
				gameScript.enabled = true;
				playersAlive = numOfPlayers;
				amIAlive = true;
				gameStart = true;
				ready = false;
				playersRdy = 0;
			}

			if(playersAlive == 1)
			{
				if(amIAlive)
				{
					networkScript.AddChatMessage(PhotonNetwork.player.name + " has won the match!");

					//Add win text screen
					PhotonNetwork.Destroy(playerControllerScript.gameObject);
					playerControllerScript = null;
					overviewCamera.enabled = true;
					SetGameState(gameState.done);
				}
				else
				{
					//Add lose text screen
				}

				gameScript.enabled = false;
			}
		}
	}

	void OnGUI()
	{
		Debug.Log ("myGameState = " + myGameState);
		if(myGameState != gameState.menu)
		{
			if(GUI.Button(new Rect( ((Screen.width/25)*24)-50, (Screen.height/25)-10, 100, 20), "Leave Room"))
			{
				if(myGameState == gameState.done && ready == true)
				{
					myPhotonView.RPC("PlayerNotRdy_RPC", PhotonTargets.AllBuffered);
					ready = false;
				}
				RemovePlayer();
				overviewCamera.enabled = true;
				networkScript.AddChatMessage(PhotonNetwork.player.name + " has left the game!");
				PhotonNetwork.LeaveRoom();
				myGameState = gameState.menu;
				spawned = false;
			}
		}

		if(myGameState == gameState.waiting)
		{
			if(ready == false)
			{
				if(GUI.Button(new Rect( (Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Ready"))
				{
					myPhotonView.RPC("PlayerRdy_RPC", PhotonTargets.AllBuffered);
					networkScript.AddChatMessage(PhotonNetwork.player.name + " is ready");
					ready = true;
				}
			}
			else
			{
				if(GUI.Button(new Rect( (Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Not Ready"))
				{
					myPhotonView.RPC("PlayerNotRdy_RPC", PhotonTargets.AllBuffered);
					networkScript.AddChatMessage(PhotonNetwork.player.name + " is not ready");
					ready = false;
				}
			}

			if(numOfPlayers > 1 && playersRdy == numOfPlayers)
			{
				SetGameState(gameState.playing);
				networkScript.AddChatMessage("Game Start!");
			}
		}
		else if(myGameState == gameState.done)
		{
			if(ready == false)
			{
				if(GUI.Button(new Rect( (Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Rematch"))
				{
					myPhotonView.RPC("PlayerRdy_RPC", PhotonTargets.AllBuffered);
					networkScript.AddChatMessage(PhotonNetwork.player.name + " has joined the rematch");
					ready = true;
				}
			}

			if(playersRdy == numOfPlayers)
			{
				playerNums.Clear();
				playersRdy = 0;
				ready = false;
				gameStart = false;
				spawned = false;
				SetGameState(gameState.waiting);
				networkScript.AddChatMessage("New Match");
			}
		}
	}

	public gameState GetGameState()
	{
		return myGameState;
	}

	public void SetGameState(gameState state)
	{
		switch(state)
		{
		case gameState.done:
			myPhotonView.RPC("GameStateDone_RPC", PhotonTargets.AllBuffered);
			break;
		case gameState.menu:
			myPhotonView.RPC("GameStateMenu_RPC", PhotonTargets.AllBuffered);
			break;
		case gameState.playing:
			myPhotonView.RPC("GameStatePlaying_RPC", PhotonTargets.AllBuffered);
			break;
		case gameState.waiting:
			myPhotonView.RPC("GameStateWaiting_RPC", PhotonTargets.AllBuffered);
			break;
		}
	}

	public int GetPlayerNum()
	{
		return numOfPlayers;
	}

	public void PlayerJoined()
	{
		Debug.Log (PhotonNetwork.player.name + " called PlayerJoined()");
		myPhotonView.RPC("PlayerJoined_RPC", PhotonTargets.AllBuffered);
		SetGameState(gameState.waiting);

		Debug.Log(myGameState);
	}

	public void CountdownDone()
	{
		playerControllerScript.enabled = true;
	}

	public void AddPlayer(PlayerData player)
	{
		myPlayerNum = player.GetPlayerNum();
		myPhotonView.RPC("AddPlayerNum_RPC", PhotonTargets.AllBuffered, myPlayerNum);
	}

	public void RemovePlayer()
	{
		int playerIndex = playerNums.IndexOf(myPlayerNum);
		myPhotonView.RPC("RemovePlayerNum_RPC", PhotonTargets.AllBuffered, playerIndex);

		if(playerControllerScript!=null)
		{
			GameObject myPlayer = playerControllerScript.gameObject;
			PhotonNetwork.Destroy(myPlayer);
			playerControllerScript = null;
		}
	}

	public void PlayerDied()
	{
		amIAlive = false;
		networkScript.AddChatMessage(PhotonNetwork.player.name + " has died");
		myPhotonView.RPC("PlayerDied_RPC", PhotonTargets.AllBuffered);

		PhotonNetwork.Destroy(playerControllerScript.gameObject);
		playerControllerScript = null;
		overviewCamera.enabled = true;
	}

	[RPC]
	private void PlayerJoined_RPC()
	{
		++numOfPlayers;
	}

	[RPC]
	private void GameStateMenu_RPC()
	{
		myGameState = gameState.menu;
	}
	[RPC]
	private void GameStateWaiting_RPC()
	{
		myGameState = gameState.waiting;
	}
	[RPC]
	private void GameStatePlaying_RPC()
	{
		myGameState = gameState.playing;
	}
	[RPC]
	private void GameStateDone_RPC()
	{
		myGameState = gameState.done;
	}

	[RPC]
	private void PlayerRdy_RPC()
	{
		++playersRdy;
	}

	[RPC]
	private void PlayerNotRdy_RPC()
	{
		--playersRdy;
	}

	[RPC]
	private void AddPlayerNum_RPC(int playerNum)
	{
		playerNums.Add(playerNum);
	}

	[RPC]
	private void RemovePlayerNum_RPC(int playerIndex)
	{
		playerNums.RemoveAt(playerIndex);
	}

	[RPC]
	private void PlayerDied_RPC()
	{
		--playersAlive;
	}
}
