using UnityEngine;
using System.Collections;

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

	private bool spawned;
	private bool ready;
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
	}

	void Start () 
	{
		myPhotonView = GetComponent<PhotonView>();
	}

	void Update () 
	{
		if(myGameState == gameState.waiting && spawned == false)
		{
			networkScript.SpawnPlayer();
			spawned = true;
		}
	}

	void OnGUI()
	{
		Debug.Log("Game State = " + myGameState);
		if(myGameState == gameState.waiting)
		{
			if(PhotonNetwork.isMasterClient)
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
			}
		}
	}

	public gameState GetGameState()
	{
		return myGameState;
	}

	public void SetGameState(gameState state)
	{
		myPhotonView.RPC("SetGameState_RPC", PhotonTargets.AllBuffered, state);
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
	
	[RPC]
	private void PlayerJoined_RPC()
	{
		++playerNum;
	}

	[RPC]
	private void SetGameState_RPC(gameState state)
	{
		myGameState = state;
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
