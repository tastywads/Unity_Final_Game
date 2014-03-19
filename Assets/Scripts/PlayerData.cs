using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour 
{
	public GameplayManager gameplayScript;

	private bool isAlive;
	private int playerNum;

	void Start () 
	{
		isAlive = true;
		playerNum = -1;
	}

	public void SetIsAlive(bool value)
	{
		isAlive = value;
	}

	public bool GetIsAlive()
	{
		return isAlive;
	}

	public void SetPlayerNum(int num)
	{
		playerNum = num;
	}
	
	public int GetPlayerNum()
	{
		return playerNum;
	}

	public void Died()
	{
		isAlive = false;

		gameplayScript.PlayerDied();
	}
}
