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

	public NetworkManager networkScript;
	public GameManager gameScript;
	public Camera overviewCamera;

	public List<PlayerController> playerControllerScripts;

	private bool spawned;
	private bool ready;
	private bool gameStart;
	private gameState myGameState;
	private int playerNum;
	private int playersRdy;
	private PhotonView myPhotonView;

	void Awake()
	{
		playerNum = 0;
		playersRdy = 0;
		myGameState = gameState.menu;
		spawned = false;
		ready = false;
		gameStart = false;
		playerControllerScripts = new List<PlayerController>();
	}

	void Start () 
	{
		myPhotonView = GetComponent<PhotonView>();
	}

	void Update () 
	{
		if(myGameState == gameState.waiting && spawned == false)
		{
			overviewCamera.enabled = false;
			networkScript.SpawnPlayer();
			spawned = true;
		}

		if(myGameState == gameState.playing && gameStart == false)
		{
			gameScript.enabled = true;
			gameStart = true;
		}
	}

	void OnGUI()
	{
		Debug.Log("Game State = " + myGameState);
		if(myGameState == gameState.waiting)
		{
			/*if(PhotonNetwork.isMasterClient)
			{
				if(playerNum > 1 && playersRdy == (playerNum-1))
				{
					if(GUI.Button(new Rect( (Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Start"))
					{
						SetGameState(gameState.playing);
						spawned = false;
					}
				}
				else
				{
					GUI.Button (new Rect((Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Waiting for Players");
				}
			}
			else
			{
				if(ready == false)
				{
					if(GUI.Button(new Rect( (Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Ready"))
					{
						myPhotonView.RPC("PlayerRdy_RPC", PhotonTargets.AllBuffered);
						ready = true;
					}
				}
				else
				{
					if(GUI.Button(new Rect( (Screen.width/6)-70, (Screen.height/2)-50, 140, 80), "Not Ready"))
					{
						myPhotonView.RPC("PlayerNotRdy_RPC", PhotonTargets.AllBuffered);
						ready = false;
					}
				}
			}*/

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

			if(playerNum > 1 && playersRdy == playerNum)
			{
				SetGameState(gameState.playing);
				myPhotonView.RPC("PlayerNotRdy_RPC", PhotonTargets.AllBuffered);
				ready = false;
				networkScript.AddChatMessage("Game Start!");
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
		return playerNum;
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
		foreach(PlayerController scripts in playerControllerScripts)
		{
			scripts.enabled = true;
		}
	}
	
	[RPC]
	private void PlayerJoined_RPC()
	{
		++playerNum;
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
}
