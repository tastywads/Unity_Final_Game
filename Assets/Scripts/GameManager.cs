using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public CountdownTimer gameTimer;
	public CountdownTimer preGameTimer;

	void Start()
	{
		gameTimer.enabled = false;
	}
	void Update () 
	{
		preGameTimer.StartTimer();

	}
}